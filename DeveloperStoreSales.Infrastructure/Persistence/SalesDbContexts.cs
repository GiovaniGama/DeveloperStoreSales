using Microsoft.EntityFrameworkCore;
using DeveloperStoreSales.Domain.Entities;
using DeveloperStoreSales.Domain.Entities.User;

namespace DeveloperStoreSales.Infrastructure.Persistence;

public class SalesDbContext(DbContextOptions<SalesDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}
