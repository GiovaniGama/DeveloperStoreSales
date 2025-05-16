using MediatR;
using DeveloperStoreSales.Domain.Entities;
using DeveloperStoreSales.Infrastructure.Persistence;
using DeveloperStoreSales.Application.User.Commands;
using Microsoft.AspNetCore.Identity;
using DeveloperStoreSales.Domain.Entities.User;

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
        var user = new AppUser
        {
            Id = request.UserId,
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
