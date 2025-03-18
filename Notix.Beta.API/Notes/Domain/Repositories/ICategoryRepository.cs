using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Notes.Domain.Repositories;

public interface ICategoryRepository : IBaseRepository<Category, int>
{
    Task<IEnumerable<Category>> ListAllAsync();
    Task<IDictionary<int, Category>> ListByMutipleIdAsync(IEnumerable<int> categoriesIds);
}