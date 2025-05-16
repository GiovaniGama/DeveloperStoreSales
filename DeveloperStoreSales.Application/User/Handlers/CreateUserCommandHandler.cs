using MediatR;
using DeveloperStoreSales.Domain.Entities;
using DeveloperStoreSales.Infrastructure.Persistence;
using DeveloperStoreSales.Application.User.Commands;
using Microsoft.AspNetCore.Identity;
using DeveloperStoreSales.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStoreSales.Application.User.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly SalesDbContext _context;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public CreateUserCommandHandler(SalesDbContext context, IPasswordHasher<AppUser> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

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

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
