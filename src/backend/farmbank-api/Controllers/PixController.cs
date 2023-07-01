using farmbank_api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace farmbank_api;

[ApiController]
[Route("api/v0/[controller]")]
public class PixController : ControllerBase
{
    private readonly IMediator _mediator;

    public PixController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("New")]
    public async Task<IActionResult>New(NewPixCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
