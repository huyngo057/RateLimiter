using Microsoft.AspNetCore.Mvc;
using RateLimiter.Services;

namespace RateLimiter.Controllers;

[Route("api/[controller]")]
public class RateLimitController : ControllerBase
{
    private readonly RateLimitTokenService _tokenService;
    public RateLimitController(RateLimitTokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpGet]
    [ActionName("Get")]
    [Route("limit")]
    public IActionResult GetLimit()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        if (_tokenService.IsTokenExceedLimit(ipAddress))
        {
           return StatusCode(StatusCodes.Status429TooManyRequests);
        }
        return Ok("Limit test");
    }
    
    [HttpGet]
    [ActionName("Get")]
    [Route("unlimit")]
    public IActionResult GetUnLimit()
    {
        return Ok("Limit test");
    }
}