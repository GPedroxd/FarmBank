using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;

namespace FarmBank.Integration;

public class QRCodeService : IQRCodeService
{
    private readonly IMercadoPagoApi _mercadoPagoApi;

    public QRCodeService(IMercadoPagoApi mercadoPagoApi)
    {
        _mercadoPagoApi = mercadoPagoApi;
    }

    public async Task<Transaction> GenerateQRCodeAsync(NewPixCommand newPixCommand)
    {
        var payment = new NewPixRequestModel() {
            Description = "Pix fazendinha",
            TransactionAmount = newPixCommand.Amount,
            Payer = new() {
                Email = newPixCommand.Email,
            }
        };

        var response = await _mercadoPagoApi.CreatePaymentAsync(payment);

        return new Transaction(
            newPixCommand.PhoneNumber, 
            newPixCommand.Email,
            newPixCommand.UserName, 
            response.TransactionId.ToString(),
            GetAmmountWithDiscount(newPixCommand.Amount),
            response.PointOfInteraction.TransactionData.QRCodeCopyPaste,
            response.PointOfInteraction.TransactionData.QRCodeBase64, response.ExpirationDate);
    }

    private decimal GetAmmountWithDiscount(decimal ammount)
        => ammount * 0.99m;
}
