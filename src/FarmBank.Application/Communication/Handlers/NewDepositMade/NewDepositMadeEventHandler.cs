using FarmBank.Core.Event.Events;
using MediatR;

namespace FarmBank.Application.Communication.Handlers.NewDepositMadeEvent;

public class NewDepositMadeEventHandler : INotificationHandler<DepositMadeEvent>
{
    public Task Handle(DepositMadeEvent notification, CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }
}
