using FarmBank.Application.BackgroundServices.CountdownBackgroundService;
using FarmBank.Application.Communication;
using FarmBank.Application.Dto;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FarmBank.Application.BackgrounService.CountdownBackgoundService;

public class CountdownBackgoundJob : IJob
{
    private readonly ICommunicatonService _wppService;
    private readonly GeneralConfigs _configs;
    public CountdownBackgoundJob(ICommunicatonService wppService, GeneralConfigs configs)
    {
        _wppService = wppService;
        _configs = configs;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if(DateTime.UtcNow > CountdownWppMessage.FARMDAY) return;

        var message = new CountdownWppMessage(DateTime.UtcNow, _configs.FrontendUrl);

        await _wppService.SendMessagemAsync(message, CancellationToken.None);
    }
}
