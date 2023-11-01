using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommandHandler : ICommandHandler<NewPixCommand, ResponseResult<QRCode>>
{
    private readonly ILogger<NewPixCommandHandler> _logger;
    private readonly ITransactionService _qrCodeService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    public NewPixCommandHandler(ITransactionService qrCodeService, ITransactionRepository transactionRepository, IMemberRepository memberRepository, ILogger<NewPixCommandHandler> logger)
    {
        _qrCodeService = qrCodeService;
        _transactionRepository = transactionRepository;
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<ResponseResult<QRCode>> Handle(NewPixCommand request, CancellationToken cancellationToken)
    {
        var validator = new NewPixCommandValidator();

        var validationResult = validator.Validate(request);

        if(!validationResult.IsValid)
            return new (validationResult.Errors);

        var member = await _memberRepository.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken);

        if(member is null)
            return new ResponseResult<QRCode>(new ValidationFailure("PhoneNumber", "No member with that number found"));

        _logger.LogInformation($"generating qr code of amount {request.Amount} to {member.Name}");

        var transaction = await _qrCodeService.GenerateQRCodeAsync(request);
        
       _logger.LogInformation($"qr code generated of amount {request.Amount} to {member.Name} with transaction id {transaction.TransactionId}");

        await _transactionRepository.InsertAsync(transaction, cancellationToken);

        return new ResponseResult<QRCode>(transaction);
    }
}
