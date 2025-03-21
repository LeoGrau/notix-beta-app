using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Auth.Domain.Services;

namespace Notix.Beta.API.Auth.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
       var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
       var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

       var claims = new[]
       {
           new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
           new Claim(JwtRegisteredClaimNames.Email, user.Email),
           new Claim(JwtRegisteredClaimNames.Name, user.FirstName + " " + user.LastName)
       };
       
       var tokenExpiration = int.Parse(_configuration["JwtSettings:TokenExpiration"]!.Trim());
       Console.WriteLine($"TokenExpiration value: {_configuration["JwtSettings:TokenExpiration"]}");

       var token = new JwtSecurityToken(
           issuer: _configuration["JwtSettings:Issuer"],
           audience: _configuration["JwtSettings:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddHours(tokenExpiration),
           signingCredentials: credentials);
        
       return new JwtSecurityTokenHandler().WriteToken(token);
    }
}