using FarmBank.Application.Communication;
using FarmBank.Application.Communication.Handlers.EventCreated;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands;

public class CreateEventWppCommandHandler : IWppCommand
{
    private readonly ILogger<CreateEventWppCommandHandler> _logger;
    private readonly ICommunicationService _communicationService;

    public CreateEventWppCommandHandler(ILogger<CreateEventWppCommandHandler> logger, ICommunicationService communicationService)
    {
        _logger = logger;
        _communicationService = communicationService;
    }

    public async Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        _logger.LogInformation("Creating event....");

        var agua = new NewEventCreatedMessage()
        {
            EndsOn = null,
            StartsOn = null,
            EventId = Guid.NewGuid(),
            EventName = args[0],
        };

        await _communicationService.SendMessagemAsync(agua, CancellationToken.None);
    }
}
