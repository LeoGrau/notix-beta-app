using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Shared.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;

namespace Notix.Beta.API.Shared.Persistence.Repositories;

public class BaseRepository<TEntity, TKey>(AppDbContext context) : IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task<TEntity?> FindAsync(TKey id)
    {
       return await DbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await DbSet.FindAsync(id) != null;
    }
}