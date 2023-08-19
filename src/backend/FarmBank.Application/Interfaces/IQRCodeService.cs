using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Models;

namespace FarmBank.Application.Interfaces;

public interface IQRCodeService
{
    Task<Transaction> GenerationQRCodeAsync(NewPixCommand newPixCommand);
}
