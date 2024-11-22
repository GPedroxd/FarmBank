using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.Pix;

public class PixWppCommandHandler : IWppCommand
{
    private readonly ILogger<PixWppCommandHandler> _logger;

    public PixWppCommandHandler(ILogger<PixWppCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        _logger.LogInformation("PIX!!!!");
        return Task.CompletedTask;
    }
}
