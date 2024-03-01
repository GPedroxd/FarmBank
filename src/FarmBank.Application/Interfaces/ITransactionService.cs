using FarmBank.Application.Commands.NewPayment;
using FarmBank.Application.Dto;
using FarmBank.Application.Models;

namespace FarmBank.Application.Interfaces;

public interface ITransactionService
{
    Task<Transaction> GeneratePaymentAsync(NewPaymentCommand newPaymentCommand);
    Task<MarcadoPagoTransactionInfo> GetTransactionAsync(string transactionId);
}
