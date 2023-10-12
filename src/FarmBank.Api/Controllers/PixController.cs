using FarmBank.Application.Base;
using FarmBank.Application.Commands.NewPix;
using FarmBank.Application.Commands.UpdateTransaction;
using FarmBank.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// EndPoit to create a new pix request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [SwaggerResponse(201, Type = typeof(QRCode))]
        [SwaggerResponse(400, Type = typeof(ResponseResult<QRCode>))]
        [HttpPost]
        public async Task<IActionResult> PixAsync(NewPixCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.IsValid)
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
}