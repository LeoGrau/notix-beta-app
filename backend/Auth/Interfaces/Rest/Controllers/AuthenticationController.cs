using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Notix.Beta.API.Auth.Domain.Services;
using Notix.Beta.API.Auth.Resources.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Notix.Beta.API.Auth.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication 🚀")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);
        if (!result.Success)
            return Unauthorized(new { message = result.Message });
        return Ok(new
        {
            message = result.Message, token = result.Token, tokenType = "Bearer", userId = result.UserId,
            expiresIn = result.ExpiresIn
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result.Success)
            return BadRequest(new { message = result.Message });
        return Ok(new { userId = result.UserId });
    }
}