using DeveloperStoreSales.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStoreSales.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = userId }, command);
        }
    }
}
