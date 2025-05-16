using MediatR;

namespace DeveloperStoreSales.Application.User.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}