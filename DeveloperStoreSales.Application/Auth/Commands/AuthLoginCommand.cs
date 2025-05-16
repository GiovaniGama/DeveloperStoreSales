using MediatR;
namespace DeveloperStoreSales.Application.Auth.Commands;

public class AuthLoginCommand : IRequest<string>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}