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

}