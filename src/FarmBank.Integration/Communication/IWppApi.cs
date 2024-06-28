using Refit;

namespace FarmBank.Integration.Communication;

public interface IWppApi
{
    [Post("/message/text?key={key}")]
    Task<SentMessageResponseModel> SendMessageAsync([AliasAs("key")] string instanceKey, [Body] SendMessageRequestModel request);

    [Get("/status")]
    Task<IApiResponse> StatusAsync();
}
