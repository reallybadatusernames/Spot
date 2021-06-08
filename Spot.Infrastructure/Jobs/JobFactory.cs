using System;

using Quartz;
using Quartz.Spi;

namespace Spot.Infrastructure.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle triggerFiredBundle,
        IScheduler scheduler)
        {
            return _serviceProvider.GetService(triggerFiredBundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) 
        {
        
        }
    }
}
