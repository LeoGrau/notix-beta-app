using Notix.Beta.API.Auth.Domain.Models;

namespace Notix.Beta.API.Auth.Domain.Services;

public interface IJwtService
{
    string GenerateToken(User user);   
}