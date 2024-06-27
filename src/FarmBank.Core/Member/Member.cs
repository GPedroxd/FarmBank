using FarmBank.Core.Base;

namespace FarmBank.Core.Member;

public class Member : AggregateRoot
{
    public Member()
    { }

    public Member(string name, string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Name = name;
        CreatedAt = DateTime.Now;
    }
    public string Name { get; init; }
    public string PhoneNumber { get; private set; }

    public override IReadOnlyCollection<DomainEventBase> Events => throw new NotImplementedException();

    public override DateTime CreatedAt { get ; init; }
    public override DateTime? UpdatedAt { get; set; }

    public override void AddEvent(DomainEventBase @event)
    {
        throw new NotImplementedException();
    }

    public override void ClearEvents()
    {
        throw new NotImplementedException();
    }

    public override void CommitChanges()
    {
        throw new NotImplementedException();
    }
}