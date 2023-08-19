using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmBank.Application.Commands.NewPix;
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
    }
}