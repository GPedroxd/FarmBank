using FarmBank.Application.Communication;
using Microsoft.Extensions.Logging;

namespace FarmBank.Integration.Communication;

public class WppService : ICommunicationService
{
    private readonly ILogger<WppService> _logger;
    private readonly IWppApi _wppApi;
    private readonly WppConfigs _configs;
    public WppService(IWppApi wppApi, WppConfigs configs, ILogger<WppService> logger)
    {
        _wppApi = wppApi;
        _configs = configs;
        _logger = logger;
    }

    public async Task SendMessagemAsync(ICommunicationMessage message, CancellationToken cancellationToken)
    {
        var requestModel = new SendMessageRequestModel()
        {
            Id = _configs.GroupId,
            Message = message.GetFormatedMessage()
        };

        _logger.LogInformation("sending message to wpp api");

        try
        {
            await _wppApi.SendMessageAsync(_configs.InstanceKey, requestModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar messagem para wpp.");
        }

        _logger.LogInformation("Message sent to api.");
    }
}
