using DeveloperStoreSales.Domain.Entities.User;
using DeveloperStoreSales.Infrastructure.Persistence;
using DeveloperStoreSales.Infrastructure.Repositories.IUser;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStoreSales.Infrastructure.Persistence.Repositories.User;

public class UserRepository(SalesDbContext context) : IUserRepository
{
    private readonly SalesDbContext _context = context;

    public async Task<AppUser> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("Usuário não encontrado.");
    }

    public async Task<AppUser> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new InvalidOperationException("Usuário já existe.");
    }

    public async Task AddAsync(AppUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
