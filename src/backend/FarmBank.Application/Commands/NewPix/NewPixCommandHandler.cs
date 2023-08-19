using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.NewPix;

public class NewPixCommandHandler : ICommandHandler<NewPixCommand>
{
    public Task<ResponseResult> Handle(NewPixCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
