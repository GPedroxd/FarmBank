using Amazon.Runtime.Internal.Util;
using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;
using Microsoft.Extensions.Logging;

namespace FarmBank.Integration;

public class QRCodeService : IQRCodeService
{
    private readonly ILogger<QRCodeService> _logger;
    private readonly IMercadoPagoApi _mercadoPagoApi;

    public QRCodeService(IMercadoPagoApi mercadoPagoApi, ILogger<QRCodeService> logger)
    {
        _mercadoPagoApi = mercadoPagoApi;
        _logger = logger;
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

        _logger.LogInformation($"sending request to Mercadopago API");

        var response = await _mercadoPagoApi.CreatePaymentAsync(payment);

        _logger.LogInformation($"transaction {response.TransactionId} created");

        return new Transaction(
            newPixCommand.PhoneNumber, 
            newPixCommand.Email,
            response.TransactionId.ToString(),
            GetAmmountWithDiscount(newPixCommand.Amount),
            response.PointOfInteraction.TransactionData.QRCodeCopyPaste,
            response.PointOfInteraction.TransactionData.QRCodeBase64, response.ExpirationDate);
    }

    private decimal GetAmmountWithDiscount(decimal ammount)
        => ammount * 0.99m;
}
