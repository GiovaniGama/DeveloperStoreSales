
using DeveloperStoreSales.Application.Auth.Handlers;
using DeveloperStoreSales.Application.User.Handlers;
using DeveloperStoreSales.Infrastructure.Persistence.Repositories.User;
using DeveloperStoreSales.Infrastructure.Repositories.IUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace DeveloperStoreSales.Application
{
    public static class ApplicationModuleDependecy
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<CreateUserCommandHandler, CreateUserCommandHandler>();
            services.AddScoped<AuthLoginCommandHandler, AuthLoginCommandHandler>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}