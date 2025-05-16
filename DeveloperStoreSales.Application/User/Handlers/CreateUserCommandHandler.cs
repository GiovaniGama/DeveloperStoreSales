using MediatR;
using DeveloperStoreSales.Domain.Entities.User;
using DeveloperStoreSales.Application.User.Commands;
using DeveloperStoreSales.Infrastructure.Repositories.IUser;
using Microsoft.AspNetCore.Identity;

namespace DeveloperStoreSales.Application.User.Handlers;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher<AppUser> passwordHasher) : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<AppUser> _passwordHasher = passwordHasher;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        Console.WriteLine($"Email: {request.Email}");
        if (existingUser != null)
        {
            throw new InvalidOperationException("Já existe um usuário cadastrado com esse email.");
        }

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = string.Empty
        };

        user.Password = _passwordHasher.HashPassword(user, request.Password);

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
