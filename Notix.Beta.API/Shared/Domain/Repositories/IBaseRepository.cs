namespace Notix.Beta.API.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity, in TKey>
{
    Task<TEntity?> FindAsync(TKey id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<bool> ExistsAsync(TKey id);
}