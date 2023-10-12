using FarmBank.Application.Interfaces;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;

namespace FarmBank.Integration;

public class WppService : IWppService
{
    private readonly IWppApi _wppApi;

    public WppService(IWppApi wppApi)
    {
        _wppApi = wppApi;
    }

    public async Task SendMessagemAsync(string message)
    {
        var requestModel = new SendMessageRequestModel()
        {
            Id = "120363188612570035@g.us",
            Message = message
        };  

        var response = await _wppApi.SendMessageAsync(requestModel);
    }
}
