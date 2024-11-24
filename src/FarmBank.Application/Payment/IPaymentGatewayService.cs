using FarmBank.Application.Transaction.Commands.NewPayment;

namespace FarmBank.Application.Payment;

public interface IPaymentGatewayService
{
    Task<PaymentCreated> GeneratePaymentAsync(NewPaymentCommand newPaymentCommand);
    Task<PaymentInformation> GetTransactionAsync(string transactionId);
}
