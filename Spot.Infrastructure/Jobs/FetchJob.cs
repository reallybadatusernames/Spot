using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Quartz;

using MediatR;

using Spot.Domain;
using Spot.Domain.Entities;
using Spot.Infrastructure.Extensions;
using Spot.Infrastructure.Command.Fetches;


namespace Spot.Infrastructure.Jobs
{
    public class FetchJob : IJob
    {
        public static string SiteUrlKey = "SITE";

        public static string SiteHttpsKey = "SITEHTTPS";

        public static string FetchIdKey = "FETCHID";

        public static string FetchUrlKey = "FETCHURL";

        public static string FetchEnabledKey = "FETCHENABLED";

        public static string FetchRedirectKey = "FETCHREDIRECTKEY";

        private readonly IServiceProvider _provider;

        private readonly IMediator _mediator;

        public FetchJob(IServiceProvider provider, IMediator mediator)
        {
            _provider = provider;
            _mediator = mediator;
        }

        public static IJobDetail Details(Fetch fetch)
        {
            return JobBuilder.Create<FetchJob>()
                .WithIdentity(fetch.ToString(), fetch.SiteId.ToString())
                .UsingJobData(SiteUrlKey, fetch.Site.Url)
                .UsingJobData(SiteHttpsKey, fetch.Site.UsesHttps)
                .UsingJobData(FetchIdKey, fetch.Id)
                .UsingJobData(FetchUrlKey, fetch.AbsolutePath)
                .UsingJobData(FetchEnabledKey, fetch.Enabled)
                .UsingJobData(FetchRedirectKey, fetch.AllowRedirect)
                .Build();
        }

        public static ITrigger Trigger(Fetch fetch)
        {
            return TriggerBuilder.Create()
                .WithIdentity(fetch.Id.ToString(), fetch.SiteId.ToString())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var fetchSite = context.JobDetail.JobDataMap.GetString(SiteUrlKey);
            var fetchId = context.JobDetail.JobDataMap.GetGuid(FetchIdKey);
            var fetchUrl = context.JobDetail.JobDataMap.GetString(FetchUrlKey);
            var fetchEnabled = context.JobDetail.JobDataMap.GetBoolean(FetchEnabledKey);
            var fetchUsesHttps = context.JobDetail.JobDataMap.GetBoolean(SiteHttpsKey);
            var fetchAllowRedirect = context.JobDetail.JobDataMap.GetBoolean(FetchRedirectKey);

            if (!fetchEnabled)
            {
                await createInactive();
            }
            else
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.AllowAutoRedirect = fetchAllowRedirect ? true : false;

                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(fetchSite.SiteUri(fetchUrl, fetchUsesHttps));

                    if (!response.IsSuccessStatusCode)
                    {
                        await createFailure(response.StatusCode);
                    }
                }
            }

            async Task createFailure(HttpStatusCode code)
            {
                using (var scope = _provider.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<SqlContext>();
                    await new CreateFetchResult.Handler(ctx).Handle(new CreateFetchResult.Command
                    {
                        FetchId = fetchId,
                        Code = code,
                        Status = FetchStatus.Error
                    }, new System.Threading.CancellationToken());
                }
            }

            async Task createInactive()
            {
                using (var scope = _provider.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<SqlContext>();
                    await new CreateFetchResult.Handler(ctx).Handle(new CreateFetchResult.Command
                    {
                        FetchId = fetchId,
                        Code = HttpStatusCode.OK,
                        Status = FetchStatus.Inactive
                    }, new System.Threading.CancellationToken());
                }
            }
        }
    }
}
