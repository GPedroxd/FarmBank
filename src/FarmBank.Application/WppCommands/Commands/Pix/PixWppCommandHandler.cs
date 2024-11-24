using FarmBank.Application.Communication;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Core.Event;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.WppCommands.Commands.Pix;

public class PixWppCommandHandler : IWppCommand
{
    private readonly ILogger<PixWppCommandHandler> _logger;
    private readonly ICommunicationService _communicationService;
    private readonly IEventRepository _eventRepository;
    private readonly IMediator _mediator;

    public PixWppCommandHandler(ILogger<PixWppCommandHandler> logger, ICommunicationService communicationService, IMediator mediator, IEventRepository eventRepository)
    {
        _logger = logger;
        _communicationService = communicationService;
        _mediator = mediator;
        _eventRepository = eventRepository;
    }

    public async Task ProcessAsync(WppInputMessage inputMessage, string[] args)
    {
        var senderPhone = inputMessage.SenderId.Split(":")[0];
        var memberPhone = senderPhone.Skip(2).ToString();
        var amount = decimal.Parse(args[0]);

        var activeEvents = (await _eventRepository.GetActivetedEventAsync(CancellationToken.None)).OrderByDescending(evnt => evnt.CreatedAt);

        Core.Event.Event @event;

        if (args.Length > 1) 
        {
            var eventName = args[1];

            @event = activeEvents.First(evnt => evnt.Name.Equals(eventName, StringComparison.InvariantCultureIgnoreCase));
        }
        else
        {
            @event = activeEvents.First();
        }

        var command = new NewPaymentCommand()
        {
            Amount = amount,
            Email = "vinibr242@hotmail.com",
            EventId = @event.Id,
            PaymentMethod = "pix",
            PhoneNumber = senderPhone,
        };

        var result = await _mediator.Send(command);

        if(result.IsValid is not true)
        {
            await _communicationService.SendMessagemAsync(new PixReplyMessage(amount, 
                "Não te achamos aqui, não. \r\n Dá !join ai pra eu ver uma coisa",
                true), inputMessage.Message.Id);
            return;
        }

        await _communicationService.SendMessagemAsync(new PixReplyMessage(amount, result.Result.PixCopyPaste), inputMessage.Message.Id);
    }
}
