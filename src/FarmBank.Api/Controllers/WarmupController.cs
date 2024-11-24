using FarmBank.Application.Member;
using FarmBank.Integration.Communication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FarmBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarmupController : ControllerBase
{
    private readonly IWppApi _wppApi;

    public WarmupController(IWppApi wppApi)
    {
        _wppApi = wppApi;
    }

    /// <summary>
    /// Warmuping endpoint
    /// </summary>
    /// <returns></returns>
    [SwaggerResponse(200)]
    [HttpGet]
    public async Task<IResult> WarmupAsync()
    {
        try
        {
            await _wppApi.StatusAsync();
        }
        catch (Exception) { }
        return TypedResults.Ok();
    }

}