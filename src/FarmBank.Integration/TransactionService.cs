using FarmBank.Application.Dto;
using FarmBank.Application.Payment;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;
using Microsoft.Extensions.Logging;

namespace FarmBank.Integration;

public class TransactionService : IPaymentGatewayService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly IMercadoPagoApi _mercadoPagoApi;

    public TransactionService(IMercadoPagoApi mercadoPagoApi, ILogger<TransactionService> logger)
    {
        _mercadoPagoApi = mercadoPagoApi;
        _logger = logger;
    }

    public async Task<PaymentCreated> GeneratePaymentAsync(NewPaymentCommand newPaymentCommand)
    {
        var payment = new NewPaymentRequestModel()
        {
            Description = "Pix fazendinha",
            TransactionAmount = newPaymentCommand.Amount,
            Payer = new() { Email = newPaymentCommand.Email, },
            PaymentMethodId = newPaymentCommand.PaymentMethod,
            Token = newPaymentCommand.Token,
            Installments = newPaymentCommand.Installments,
            IssuerId = newPaymentCommand.IssuerId
        };

        _logger.LogInformation($"sending request to Mercadopago API");

        var response = await _mercadoPagoApi.CreatePaymentAsync(payment);

        _logger.LogInformation($"transaction {response.TransactionId} created");

        return new PaymentCreated(
            response.TransactionId.ToString(),
            newPaymentCommand.Email,
            newPaymentCommand.PhoneNumber,
            GetAmmountWithDiscount(newPaymentCommand.Amount),
            0.01m,
            response.PointOfInteraction.TransactionData.QRCodeCopyPaste,
            response.PointOfInteraction.TransactionData.QRCodeBase64,
            response.ExpirationDate
        );
    }

    public async Task<PaymentInformation> GetTransactionAsync(string transactionId) =>
        await _mercadoPagoApi.GetPaymentAsync(transactionId);

    private decimal GetAmmountWithDiscount(decimal ammount) => ammount * 0.99m;
}
