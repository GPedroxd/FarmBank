using FarmBank.Application.Commands.NewMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> NewMemberAsync(NewMemberCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsValid)
            return Ok(result);

        return BadRequest(result);
    }

}