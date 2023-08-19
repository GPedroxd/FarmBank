using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;
using FarmBank.Integration.ResponseModel;
using Refit;

namespace FarmBank.Integration;

public class QRCodeService : IQRCodeService
{
    private readonly IMercadoPagoApi _mercadoPagoApi;

    public QRCodeService(IMercadoPagoApi mercadoPagoApi)
    {
        _mercadoPagoApi = mercadoPagoApi;
    }

    public async Task<Transaction> GenerationQRCodeAsync(NewPixCommand newPixCommand)
    {
        var payment = new NewPixRequestModel() {
            Description = "Pix fazendinha",
            TransactionAmount = newPixCommand.Amount,
            Payer = new() {
                Email = newPixCommand.Email,
            }
        };

        var response = await _mercadoPagoApi.CreatePaymentAsync(payment);

        return null;
    }
}
