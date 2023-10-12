using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;
using FluentValidation.Results;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommandHandler : ICommandHandler<NewPixCommand, ResponseResult<QRCode>>
{
    private readonly IQRCodeService _qrCodeService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMemberRepository _memberRepository;
    public NewPixCommandHandler(IQRCodeService qrCodeService, ITransactionRepository transactionRepository, IMemberRepository memberRepository)
    {
        _qrCodeService = qrCodeService;
        _transactionRepository = transactionRepository;
        _memberRepository = memberRepository;
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

        var transaction = await _qrCodeService.GenerateQRCodeAsync(request);
        //var transaction = new Models.Transaction(request.PhoneNumber, "1234","12345", request.Amount,"","", DateTime.Now);
       
        await _transactionRepository.InsertAsync(transaction, cancellationToken);

        return new ResponseResult<QRCode>(transaction);
    }
}
