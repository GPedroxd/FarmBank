using FarmBank.Application.WppCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WppController : ControllerBase
{
    private readonly IMediator _mediator;

    public WppController(IMediator mediator)
        => _mediator = mediator;


    [HttpPost("callback")]
    public async Task<IActionResult> CallBackAsync(WppInputMessage wppCommand)
    {
        await _mediator.Send(wppCommand);

        return Ok();
    }
}
