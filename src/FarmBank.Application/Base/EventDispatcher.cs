using FarmBank.Core.Base;
using MediatR;

namespace FarmBank.Application.Base;

public class EventDispatcher
{
    private readonly IMediator _mediator;

    public EventDispatcher(IMediator mediator)
        =>  _mediator = mediator;
    
    public async Task DispatchEvents(AggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        foreach (var @event in aggregate.Events) 
            await _mediator.Publish(@event, cancellationToken);
       
        aggregate.ClearEvents();
    }
}
