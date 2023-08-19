using FarmBank.Integration.RequestModel;
using FarmBank.Integration.ResponseModel;
using Refit;

namespace FarmBank.Integration.Interfaces;

[Headers("Authorization: Bearer")]
public interface IMercadoPagoApi
{
    [Post("/v1/payments")]
    Task<QRCodeResponseModel> CreatePaymentAsync([Body]NewPixRequestModel request );
}
