using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Auth.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;
using Notix.Beta.API.Shared.Persistence.Repositories;

namespace Notix.Beta.API.Auth.Repositories;

public class UserRepository(AppDbContext context) :  BaseRepository<User, int>(context), IUserRepository
{
    public async Task<User?> FindUserByUsernameAsync(string email)
    {
        return await DbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
}