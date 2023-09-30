using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommandHandler : ICommandHandler<NewPixCommand, ResponseResult<QRCode>>
{
    private readonly IQRCodeService _qrCodeService;
    private readonly ITransactionRepository _transactionRepository;
    public NewPixCommandHandler(IQRCodeService qrCodeService, ITransactionRepository transactionRepository)
    {
        _qrCodeService = qrCodeService;
        _transactionRepository = transactionRepository;
    }

    public async Task<ResponseResult<QRCode>> Handle(NewPixCommand request, CancellationToken cancellationToken)
    {
        // var transaction = await _qrCodeService.GenerateQRCodeAsync(request);
        var transaction = new Models.Transaction("","","","",0,"","", DateTime.Now);
       
        await _transactionRepository.InsertAsync(transaction, cancellationToken);

        return new ResponseResult<QRCode>(transaction);
    }
}
