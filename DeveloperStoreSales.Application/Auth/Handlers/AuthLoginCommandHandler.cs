using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeveloperStoreSales.Application.Auth.Commands;
using DeveloperStoreSales.Domain.Entities.User;
using DeveloperStoreSales.Infrastructure.Repositories.IUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeveloperStoreSales.Application.Auth.Handlers;

public class AuthLoginCommandHandler(IUserRepository userRepository, IPasswordHasher<AppUser> passwordHasher, IConfiguration configuration) : IRequestHandler<AuthLoginCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<AppUser> _passwordHasher = passwordHasher;
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email) ?? throw new Exception("Usuário não encontrado");
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Senha inválida");

        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
