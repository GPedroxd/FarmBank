using FarmBank.Application.Event.Commands.CreateEvent;
using FarmBank.Application.Event.Queries.GetAvailablesEventsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
        => _mediator = mediator;
    /// <summary>
    /// Create a new Event
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> NewEventAsync(CreateEventCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if(!result.IsValid)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Return all availables events
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEventsAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAvailablesEventsQuery(), cancellationToken);

        return Ok(result);
    }
}
