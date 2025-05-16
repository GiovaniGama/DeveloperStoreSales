using DeveloperStoreSales.Domain.Entities.User;

namespace DeveloperStoreSales.Infrastructure.Repositories.IUser;

public interface IUserRepository
{
    Task<AppUser> GetByIdAsync(Guid id);
    Task<AppUser> GetByEmailAsync(string email);
    Task AddAsync(AppUser user);

}