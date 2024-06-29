using FarmBank.Application.Payment;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Core.Transaction.Enums;
using Microsoft.Extensions.Logging;

namespace FarmBank.Integration.PaymentGateway;

public class MercadoPagoPaymentGateway : IPaymentGatewayService
{
    private const decimal PIX_DISCOUNT = .01m;
    private const decimal CC_DISCOUNT = .05M;

    private readonly ILogger<MercadoPagoPaymentGateway> _logger;
    private readonly IMercadoPagoApi _mercadoPagoApi;

    public MercadoPagoPaymentGateway(IMercadoPagoApi mercadoPagoApi, ILogger<MercadoPagoPaymentGateway> logger)
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

        _logger.LogInformation("transaction {TransactionId} created", response.TransactionId);

        return GetPayment(newPaymentCommand, response);
    }

    public async Task<PaymentInformation> GetTransactionAsync(string transactionId) =>
        await _mercadoPagoApi.GetPaymentAsync(transactionId);

    private PaymentCreated GetPayment(NewPaymentCommand command, QRCodeResponseModel response)
    {
        var paymentMethod = "pix".Equals(command.PaymentMethod) ? PaymentMethod.Pix : PaymentMethod.CreditCard;
        var discountPercentage = paymentMethod == PaymentMethod.Pix ? PIX_DISCOUNT : CC_DISCOUNT;
        var amounWithDiscount = command.Amount * (100 - discountPercentage);

        return new PaymentCreated(
            command.EventId,
            response.TransactionId.ToString(),
            command.Email,
            command.PhoneNumber,
            amounWithDiscount,
            discountPercentage,
                    response.PointOfInteraction.TransactionData.QRCodeCopyPaste,
                    response.PointOfInteraction.TransactionData.QRCodeBase64,
                    response.ExpirationDate
            );
    }
}
