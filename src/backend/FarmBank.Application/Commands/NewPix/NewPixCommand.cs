using System.Text.Json.Serialization;
using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommand : ICommand
{
    [JsonConstructor]
    public NewPixCommand(string phoneNumber, string email, double value)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        Value = value;
    }

    public string PhoneNumber { get; init; }
    public string Email {get; init; }
    public double Value { get; init; }
}
