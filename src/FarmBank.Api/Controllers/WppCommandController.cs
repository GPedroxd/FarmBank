using FarmBank.Application.Commands.WppCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WppCommandController : ControllerBase
{
    private readonly IMediator _mediator;

    public WppCommandController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// endpoint to receive wpp callback message
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> WppCallback(WppCommand wppCommand)
    {
        await _mediator.Send(wppCommand);

        return Ok();
    }
}
