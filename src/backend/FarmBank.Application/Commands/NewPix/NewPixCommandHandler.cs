using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.Interfaces;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommandHandler : ICommandHandler<NewPixCommand, ResponseResult<QRCode>>
{
    private readonly IQRCodeService _qrCodeService;

    public NewPixCommandHandler(IQRCodeService qrCodeService)
    {
        _qrCodeService = qrCodeService;
    }

    public async Task<ResponseResult<QRCode>> Handle(NewPixCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _qrCodeService.GenerateQRCodeAsync(request);
        //todo save transacation in the database

        return new ResponseResult<QRCode>(transaction);
    }
}
