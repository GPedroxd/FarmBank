using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;
using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FluentValidation;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommand : ICommand<ResponseResult<QRCode>>
{
    [JsonConstructor]
    public NewPixCommand(string phoneNumber, string email, decimal amount)
    {
        PhoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
        Email = email;
        Amount = amount;
    }

    public string PhoneNumber { get; init; }
    public string Email {get; init; }
    public decimal Amount { get; init; }
}

public class NewPixCommandValidator : AbstractValidator<NewPixCommand>
{
    public NewPixCommandValidator()
    {
        RuleFor(px => px.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(px => px.PhoneNumber).NotNull().NotEmpty().Length(11);
        RuleFor(px => px.Amount).NotEmpty().NotEmpty().GreaterThan(9);
    }
}