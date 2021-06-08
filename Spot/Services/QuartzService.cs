using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using Spot.Domain;
using Spot.Domain.Entities;
using Spot.Infrastructure.Command.Fetches;
using Spot.Infrastructure.Jobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spot.Services
{
    public class QuartzService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly IServiceScopeFactory scopeFactory;

        public QuartzService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IServiceScopeFactory scopeFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
            this.scopeFactory = scopeFactory;
        }

        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;

            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SqlContext>();
                var response = await new GetFetches.Handler(context).Handle(new GetFetches.Query(), new CancellationToken());

                Scheduler = await schedulerFactory.GetScheduler();
                Scheduler.JobFactory = jobFactory;

                foreach (var fetch in response.Fetches)
                {
                    var details = FetchJob.Details(fetch);
                    var trigger = FetchJob.Trigger(fetch);

                    await Scheduler.ScheduleJob(details, trigger, cancellationToken);
                }
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task UpdateJob(Fetch fetch)
        {
            if (Scheduler == null)
                return;


            var details = FetchJob.Details(fetch);
            var trigger = FetchJob.Trigger(fetch);

            await Scheduler.DeleteJob(details.Key);

            await Scheduler.ScheduleJob(details, trigger);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }

    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle triggerFiredBundle,
        IScheduler scheduler)
        {
            var jobDetail = triggerFiredBundle.JobDetail;
            return (IJob)_serviceProvider.GetService(jobDetail.JobType);
        }
        public void ReturnJob(IJob job) { }
    }
}
