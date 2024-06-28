using FarmBank.Application.Base;
using FarmBank.Application.Transaction.Commands.NewPayment;
using FarmBank.Application.Transaction.Commands.UpdateTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// EndPoit to create a new payment request
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [SwaggerResponse(201, Type = typeof(QRCode))]
    [SwaggerResponse(400, Type = typeof(ResponseResult<QRCode>))]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] NewPaymentCommand payment)
    {
        var result = await _mediator.Send(payment);

        if (result.IsValid)
            return StatusCode(201, result.Result);

        return BadRequest(result);
    }

    [HttpPost("callback")]
    public async Task<IActionResult> CallBackAsync(UpdateTransactionCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
