using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;
using Notix.Beta.API.Shared.Persistence.Repositories;

namespace Notix.Beta.API.Notes.Repositories;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category, int>(context), ICategoryRepository
{
    public async Task<IEnumerable<Category>> ListAllAsync()
    {
        return await DbSet.ToListAsync();
    }
    
    public async Task<IDictionary<int, Category>> ListByMutipleIdAsync(IEnumerable<int> categoriesIds)
    {
        return await DbSet.Where(c => categoriesIds.Contains(c.Id)).ToDictionaryAsync(category => category.Id, category => category);
    }
}