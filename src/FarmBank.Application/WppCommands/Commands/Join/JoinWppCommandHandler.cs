using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.Join;

public class JoinWppCommandHandler : IWppCommand
{
    private readonly ILogger<JoinWppCommandHandler> _logger;

    public JoinWppCommandHandler(ILogger<JoinWppCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        _logger.LogInformation("point reached!!");
        return Task.CompletedTask;
    }
}
