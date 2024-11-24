using FarmBank.Application.Base;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Application.WppCommands;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FarmBank.Application;

public static class IoC
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(
        conf => conf.RegisterServicesFromAssemblyContaining<NewPaymentCommand>()
        ); 
        
        services.AddScoped<EventDispatcher>();
        services.AddSingleton<WppCommandHandlerFactory>();
        
        //services.AddQuartz(opts =>
        //{
        //    opts.AddJob<QueryCreditCardTransactionStatusJobs>(JobKey.Create(nameof(QueryCreditCardTransactionStatusJobs)))
        //    .AddTrigger(trigger =>
        //        trigger.ForJob(JobKey.Create(nameof(QueryCreditCardTransactionStatusJobs)))
        //    );
        //});
        //services.AddQuartzHostedService();

        services.AddWppCommands();
        
        return services;
    }
}
