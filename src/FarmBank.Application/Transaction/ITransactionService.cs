using FarmBank.Application.Dto;
using FarmBank.Application.Event.Commands.NewPayment;

namespace FarmBank.Application.Transaction;

public interface ITransactionService
{
    Task<Transaction> GeneratePaymentAsync(NewPaymentCommand newPaymentCommand);
    Task<MarcadoPagoTransactionInfo> GetTransactionAsync(string transactionId);
}
