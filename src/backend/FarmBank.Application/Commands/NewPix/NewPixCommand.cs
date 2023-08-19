using System.Text.Json.Serialization;
using FarmBank.Application.Base;
using FarmBank.Application.Dto;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommand : ICommand<ResponseResult<QRCode>>
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
