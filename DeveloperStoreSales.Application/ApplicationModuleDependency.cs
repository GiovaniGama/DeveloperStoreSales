
using DeveloperStoreSales.Application.User.Handlers;
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


            return services;
        }
    }
}