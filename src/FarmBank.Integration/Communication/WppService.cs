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

    public async Task SendMessagemAsync(ICommunicationMessage message,string? replyTo = null, CancellationToken cancellationToken = default)
    {
        var requestModel = new SendMessageRequestModel()
        {
            Phone = _configs.GroupId,
            Message = message.GetFormatedMessage().Replace("@LINK", _configs.FrontendUrl),
            ReplyTo = replyTo
        };
        
        _logger.LogInformation("sending message to wpp api");

        try
        {
            var response = await _wppApi.SendMessageAsync(requestModel);

            _logger.LogInformation("Message sent to api.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar messagem para wpp.");
        }
    }
}
