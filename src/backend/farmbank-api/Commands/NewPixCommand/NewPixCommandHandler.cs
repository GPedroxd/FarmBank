using farmbank_api.Dtos;
using MediatR;

namespace farmbank_api.Commands;
public class NewPixCommandHandler : IRequestHandler<NewPixCommand, PixCreated>
{
    private readonly IMediator _mediator;

    public NewPixCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<PixCreated> Handle(NewPixCommand request, CancellationToken cancellationToken)
    {
        // do the logic to create a pix

        var createTransactionCommand = new CreateTransactionCommand("", "", "", 0.1m);
        await _mediator.Send(createTransactionCommand, cancellationToken);

        var pixCreated = new PixCreated("pix criado", "pixcriado img base64");
        return pixCreated;
    }
}