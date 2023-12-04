namespace FarmBank.Application.WppCommands;

public struct WppSenderInfo
{
    public WppSenderInfo(string memberName, string phoneNumber)
    {
        MemberName = memberName;
        PhoneNumber = phoneNumber;
    }

    public string MemberName { get; init; }
    public string PhoneNumber { get; init; }
}
