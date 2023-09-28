using FarmBank.Application.Base;

namespace FarmBank.Application.Commands.SendWppMessage;

public class SendWppMessageCommandHandler : ICommandHandler<SendWppMessageCommand>
{
    public async Task Handle(SendWppMessageCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
