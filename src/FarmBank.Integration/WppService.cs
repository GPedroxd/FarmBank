using Amazon.Runtime.Internal.Util;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FarmBank.Integration.Interfaces;
using FarmBank.Integration.RequestModel;
using Microsoft.Extensions.Logging;

namespace FarmBank.Integration;

public class WppService : IWppService
{
    private readonly ILogger<WppService> _logger;
    private readonly IWppApi _wppApi;
    private readonly GeneralConfigs _configs;
    public WppService(IWppApi wppApi, GeneralConfigs configs, ILogger<WppService> logger)
    {
        _wppApi = wppApi;
        _configs = configs;
        _logger = logger;
    }

    public async Task SendMessagemAsync(string message)
    {
        var requestModel = new SendMessageRequestModel()
        {
            Id = _configs.GroupId,
            Message = message
        }; 

        _logger.LogInformation("sending message to wpp api");

        var response = await _wppApi.SendMessageAsync(_configs.InstanceKey, requestModel);
    }
}
