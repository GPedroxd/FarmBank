using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;

namespace FarmBank.Integration;

public class QRCodeService : IQRCodeService
{
    public Task<Transaction> GenerationQRCodeAsync(NewPixCommand newPixCommand)
    {
        throw new NotImplementedException();
    }
}
