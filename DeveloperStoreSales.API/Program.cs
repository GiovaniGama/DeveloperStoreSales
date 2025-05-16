using MediatR;
using DeveloperStoreSales.Application;
using Microsoft.AspNetCore.Identity;
using DeveloperStoreSales.Domain.Entities;
using DeveloperStoreSales.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using DeveloperStoreSales.Domain.Entities.User;
using DeveloperStoreSales.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();