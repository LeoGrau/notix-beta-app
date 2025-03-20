using Notix.Beta.API.Auth.Domain.Services.Communication;
using Notix.Beta.API.Auth.Resources.Auth;

namespace Notix.Beta.API.Auth.Domain.Services;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(string email, string password);
    public Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
}