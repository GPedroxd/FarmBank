using FarmBank.Application.Communication;
using FarmBank.Application.Communication.Handlers.EventCreated;
using FarmBank.Core.Event;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.CreatEvent;

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
        var eventName = args[0].Trim().Replace("-", " ");

        DateTime? startDate = null;
        DateTime? endDate = null;

        if (args.Length > 1)
        {
            startDate = DateTime.Parse(args[1]);
            endDate = DateTime.Parse(args[2]);
        }

        _logger.LogInformation("Creating event...");

        var agua = new NewEventCreatedMessage()
        {
            EndsOn = null,
            StartsOn = null,
            EventId = Guid.NewGuid(),
            EventName = eventName,
        };

        await _communicationService.SendMessagemAsync(agua, cancellationToken: CancellationToken.None);
    }
}
