using FarmBank.Application.Base;
using FarmBank.Application.Payment;
using FarmBank.Core.Member;
using FarmBank.Core.Transaction;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Transaction.Commands.NewPayment;

public class NewPaymentCommandHandler : ICommandHandler<NewPaymentCommand, ResponseResult<QRCode>>
{
    private readonly ILogger<NewPaymentCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPaymentGatewayService _transactionService;

    public NewPaymentCommandHandler(
        IMemberRepository memberRepository,
        ILogger<NewPaymentCommandHandler> logger,
        ITransactionRepository transactionRepository,
        IPaymentGatewayService transactionService
    )
    {
        _memberRepository = memberRepository;
        _logger = logger;
        _transactionRepository = transactionRepository;
        _transactionService = transactionService;
    }

    public async Task<ResponseResult<QRCode>> Handle(
        NewPaymentCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new NewPaymentCommandValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return new ResponseResult<QRCode>(validationResult.Errors);

        var member = await _memberRepository.GetByPhoneNumberAsync(
            request.PhoneNumber,
            cancellationToken
        );

        if (member is null)
            return new ResponseResult<QRCode>(
                new ValidationFailure("PhoneNumber", "No member with that number found")
            );

        _logger.LogInformation("generating qr code of amount {amount} to {memberName}", request.Amount, member.Name);

        var paymentInformation = await _transactionService.GeneratePaymentAsync(request);

        _logger.LogInformation(
            "qr code generated of amount {amount} to {memberName} with transaction id {transactionId}.", request.Amount, member.Name, paymentInformation.TransactionId
        );

        Core.Transaction.Transaction transaction = paymentInformation;

        await _transactionRepository.InsertAsync(transaction, cancellationToken);

        return new ResponseResult<QRCode>(paymentInformation.GetQrInformation());
    }
}
