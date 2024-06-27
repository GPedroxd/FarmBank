using FarmBank.Application.Base;
using FarmBank.Application.Member;
using FarmBank.Application.Member.Commands.NewMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    /// <summary>
    /// EndPoint to insert a new member to farm bank
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [SwaggerResponse(201, Type = typeof(Member))]
    [SwaggerResponse(400, Type = typeof(ResponseResult<Member>))]
    [HttpPost]
    public async Task<IActionResult> NewMemberAsync(NewMemberCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsValid)
            return StatusCode(201, result.Result);

        return BadRequest(result);
    }

}