using FarmBank.Integration.RequestModel;
using FarmBank.Integration.ResponseModel;
using Refit;

namespace FarmBank.Integration.Interfaces;

public interface IWppApi
{
    [Post("/message/text?key=321")]
    Task<SentMessageResponseModel> SendMessageAsync([Body]SendMessageRequestModel request );
}
