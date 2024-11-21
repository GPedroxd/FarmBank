using FarmBank.Core.Event;
using FarmBank.Core.Event.Events;
using MediatR;

namespace FarmBank.Application.Communication.Handlers.NewDepositMade;

public class NewDepositMadeEventHandler : INotificationHandler<DepositMadeEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly ICommunicationService _communicationService;

    public NewDepositMadeEventHandler(IEventRepository eventRepository, ICommunicationService communicationService)
    {
        _eventRepository = eventRepository;
        _communicationService = communicationService;
    }

    public async Task Handle(DepositMadeEvent notification, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(notification.EventId, cancellationToken);

        var grouppedDepositByMember = @event.Deposits.GroupBy(gb => gb.MemberId);

        var depositsSummarized = grouppedDepositByMember.Select(s => {
            var sub = s.First();

            return new 
            {
                MemberId = s.Key,
                sub.MemberName,
                Amount = s.Sum(s => s.Amount)
            }; 
                
        });

        var depositOrderd = depositsSummarized.OrderByDescending(s => s.Amount).ToList();

        var totalDeposited = depositOrderd.First(f => f.MemberId == notification.MemberId).Amount;

        var placement = depositOrderd.FindIndex(f => f.MemberId == notification.MemberId);

        var newDepositInfo = new NewDeposit()
        {
            TotalDeposited = totalDeposited,
            MemberName = notification.MemberName,
            AmountNewDosit = notification.Amount,
            Placement = placement
        };

        var newDepositedMessage = new NewDepositMadeMessage()
        {
            EventName = @event.Name,
            NewDepositInformation = newDepositInfo,
            Placements = depositOrderd.Take(10).Select(s =>  new Deposit() { MemberName = s.MemberName, TotalDeposited = s.Amount })
        };

        await _communicationService.SendMessagemAsync(newDepositedMessage, cancellationToken: cancellationToken);
    }
}
