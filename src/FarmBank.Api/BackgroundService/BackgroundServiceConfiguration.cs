using FarmBank.Application.BackgrounService.CountdownBackgoundService;
using Quartz;

namespace FarmBank.Api.BackgroundService;

public static class BackgroundServiceConfiguration
{
    public static void AddBackgroundService(this IServiceCollection services)
    {
        services.AddQuartz(opts =>
        {
            var jobKey = JobKey.Create(nameof(CountdownBackgoundJob));
            opts.AddJob<CountdownBackgoundJob>(jobKey)
                .AddTrigger(triggerconfig => 
                    triggerconfig.ForJob(jobKey)
                    .WithCronSchedule("0 0 4 * * ?"));
        });

        services.AddQuartzHostedService(opts =>
        {
            opts.WaitForJobsToComplete = true;
        });
    }
}
