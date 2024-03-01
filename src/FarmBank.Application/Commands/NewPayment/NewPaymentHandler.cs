using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FarmBank.Application.Models;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.NewPayment;

public class NewPaymentCommandHandler : ICommandHandler<NewPaymentCommand, ResponseResult<QRCode>>
{
    private readonly ILogger<NewPaymentCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionService _transactionService;

    public NewPaymentCommandHandler(
        IMemberRepository memberRepository,
        ILogger<NewPaymentCommandHandler> logger,
        ITransactionRepository transactionRepository,
        ITransactionService transactionService
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

        _logger.LogInformation($"generating qr code of amount {request.Amount} to {member.Name}");

        var transaction = await _transactionService.GeneratePaymentAsync(request);

        _logger.LogInformation(
            $"qr code generated of amount {request.Amount} to {member.Name} with transaction id {transaction.TransactionId}"
        );

        await _transactionRepository.InsertAsync(transaction, cancellationToken);

        return new ResponseResult<QRCode>(transaction);
    }
}
