using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Dto;
using FarmBank.Application.Models;

namespace FarmBank.Application.Interfaces;

public interface ITransactionService
{
    Task<Transaction> GenerateQRCodeAsync(NewPixCommand newPixCommand);
    Task<MarcadoPagoTransactionInfo> GetTransactionAsync(string transactionId);
}
