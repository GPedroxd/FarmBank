using FarmBank.Core.Base;

namespace FarmBank.Core.Event.Events;

public class DepositMadeEvent : DomainEventBase
{
    public DepositMadeEvent(Guid memberId, string memberName, Guid eventId, decimal amount)
    {
        MemberId = memberId;
        MemberName = memberName;
        EventId = eventId;
        Amount = amount;
    }
    public Guid MemberId { get; init; }
    public string MemberName { get; init; }
    public Guid EventId { get; init; }
    public decimal Amount { get; init; }
}
