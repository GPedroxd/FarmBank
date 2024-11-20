using FarmBank.Application.Payment;
using Quartz;

namespace FarmBank.Application.Transaction.BackgroundJobs;

public class QueryCreditCardTransactionStatusJobs : IJob
{
    private readonly IPaymentGatewayService _paymentGatewayService;

    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
