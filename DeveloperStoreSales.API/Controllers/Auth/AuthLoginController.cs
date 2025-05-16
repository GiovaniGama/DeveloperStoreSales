using DeveloperStoreSales.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStoreSales.API.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthLoginController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthLoginCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
