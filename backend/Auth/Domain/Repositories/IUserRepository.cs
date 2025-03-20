using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Auth.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User, int>
{
    Task<User?> FindUserByUsernameAsync(string email);
}