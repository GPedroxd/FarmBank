using FarmBank.Application.Base;
using FluentValidation;

namespace FarmBank.Application.Transaction.Commands.NewPayment;

public class NewPaymentCommand : ICommand<ResponseResult<QRCode>>
{
    public Guid EventId { get; set; }
    public string Token { get; set; }
    public decimal Amount { get; set; }
    public int Installments { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PaymentMethod { get; set; }
    public string IssuerId { get; set; }
}

public class NewPaymentCommandValidator : AbstractValidator<NewPaymentCommand>
{
    public NewPaymentCommandValidator()
    {
        RuleFor(c => c.PaymentMethod).NotNull().NotEmpty();
        RuleFor(c => c.IssuerId)
            .NotNull()
            .NotEmpty()
            .When(c => c.PaymentMethod != "pix", ApplyConditionTo.AllValidators);
        RuleFor(c => c.Token)
            .NotNull()
            .NotEmpty()
            .When(c => c.PaymentMethod != "pix", ApplyConditionTo.AllValidators);
        RuleFor(c => c.Installments)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .When(c => c.PaymentMethod != "pix", ApplyConditionTo.AllValidators);
        RuleFor(c => c.Amount).NotEmpty().NotEmpty().GreaterThan(0);
        RuleFor(px => px.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(px => px.PhoneNumber).NotNull().NotEmpty().Length(11);
    }
}
