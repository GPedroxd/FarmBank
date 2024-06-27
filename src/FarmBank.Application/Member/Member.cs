using FarmBank.Application.Base;

namespace FarmBank.Application.Member;

public class Member : IBaseEntity
{
    public Member()
    { }

    public Member(string name, string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Name = name;
    }
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string Name { get; init; }
    public string PhoneNumber { get; private set; }
}