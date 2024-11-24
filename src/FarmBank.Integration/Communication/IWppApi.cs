using Refit;

namespace FarmBank.Integration.Communication;

[Headers("Authorization: Basic")]
public interface IWppApi
{
    [Post("/send/message")]
    Task<SentMessageResponseModel> SendMessageAsync([Body] SendMessageRequestModel request);

    [Get("/status")]
    Task<IApiResponse> StatusAsync();
}
