using System.Text.Json.Serialization;
using FarmBank.Application.Base;
using FarmBank.Application.Models;
using FluentValidation;

namespace FarmBank.Application.Commands.NewMember;

public class NewMemberCommand : ICommand<ResponseResult<Member>>
{
    [JsonConstructor]
    public NewMemberCommand(string name, string phoneNumber)
    {
        PhoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");;
        Name = name;
    }

    public string PhoneNumber { get; init; }
    public string Name { get; init; }
}

public class NewMemberCommandValidator : AbstractValidator<NewMemberCommand>
{
    public NewMemberCommandValidator()
    {  
            RuleFor(mb => mb.PhoneNumber).NotNull().NotEmpty().Length(11);
            RuleFor(mb => mb.Name).NotNull().NotEmpty().MinimumLength(3);
    }
}

