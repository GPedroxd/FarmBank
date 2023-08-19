using System.Text.Json.Serialization;
using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommand : ICommand
{
    [JsonConstructor]
    public NewPixCommand(string phoneNumber, string email, double amount)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        Amount = amount;
    }

    public string PhoneNumber { get; init; }
    public string Email {get; init; }
    public double Amount { get; init; }
}
