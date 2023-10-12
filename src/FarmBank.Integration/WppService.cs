using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;

namespace FarmBank.Integration;

public class WppService : IWppService
{
    private readonly IWppApi _wppApi;
    private readonly GeneralConfigs _configs;
    public WppService(IWppApi wppApi, GeneralConfigs configs)
    {
        _wppApi = wppApi;
        _configs = configs;
    }

    public async Task SendMessagemAsync(string message)
    {
        var requestModel = new SendMessageRequestModel()
        {
            Id = _configs.GroupId,
            Message = message
        };  

        var response = await _wppApi.SendMessageAsync(_configs.InstanceKey, requestModel);
    }
}
