using FarmBank.Application.Base;
using FarmBank.Application.Communication;
using FarmBank.Application.Communication.Handlers.EventCreated;
using FarmBank.Core.Event;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.CreateEvent;

public class CreateEventWppCommandHandler : IWppCommand
{
    private readonly ILogger<CreateEventWppCommandHandler> _logger;
    private readonly ICommunicationService _communicationService;
    private readonly IEventRepository _eventRepository;

    public CreateEventWppCommandHandler(ILogger<CreateEventWppCommandHandler> logger, ICommunicationService communicationService, IEventRepository eventRepository)
    {
        _logger = logger;
        _communicationService = communicationService;
        _eventRepository = eventRepository;
    }

    public async Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        if (IsAdmNumber(inputMessage.SenderId) is not true)
        {
            await _communicationService.SendMessagemAsync(new CommandNotFoundDefaultMessage(), inputMessage.Message.Id);
            return;
        }

        var eventName = args[0].Trim().Replace("-", " ");

        DateTime? startDate = null;
        DateTime? endDate = null;

        if (args.Length > 1)
        {
            startDate = DateTime.Parse(args[1]);
            endDate = DateTime.Parse(args[2]);
        }

        _logger.LogInformation("Creating event {event} at {date}.", eventName, DateTime.Now);

        var @event = new Core.Event.Event(eventName, startDate, endDate);   

        await _eventRepository.InsertAsync(@event, CancellationToken.None); 

        await SendEventCreatedMessageAsync(eventName);
    }

    private async Task SendEventCreatedMessageAsync(string eventName)
    {
        var createdEventMessage = new NewEventCreatedMessage()
        {
            EndsOn = null,
            StartsOn = null,
            EventId = Guid.NewGuid(),
            EventName = eventName,
        };

        await _communicationService.SendMessagemAsync(createdEventMessage, cancellationToken: CancellationToken.None);
    }

    private bool IsAdmNumber(string senderId)
    {
        var senderNumber = senderId.Split(":")[0];

        return CONST.ADMNUMBERS.Contains(senderNumber);
    }
}
