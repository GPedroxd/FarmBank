using FarmBank.Application.Payment;
using Refit;

namespace FarmBank.Integration.PaymentGateway;

[Headers("Authorization: Bearer")]
public interface IMercadoPagoApi
{
    [Post("/v1/payments")]
    Task<QRCodeResponseModel> CreatePaymentAsync([Body] NewPaymentRequestModel request);

    [Get("/v1/payments/{id}")]
    Task<PaymentInformation> GetPaymentAsync(string id);
}
