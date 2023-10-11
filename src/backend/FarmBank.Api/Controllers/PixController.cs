using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Commands.UpdateTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PixController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PixController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PixAsync(NewPixCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.IsValid)
                return Ok(result);
            
            return BadRequest(result);
        }

        [HttpPost("callback")]
        public async Task<IActionResult> CallBackAsync(UpdateTransactionCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}