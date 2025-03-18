using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Services.Communication;

namespace Notix.Beta.API.Notes.Domain.Services;

public interface ICategoryService
{
    Task<CategoryResponse> FindAsync(int id);
    Task<IEnumerable<Category>> ListAllAsync();
    Task<CategoryResponse> CreateAsync(Category newCategory);
    Task<CategoryResponse> UpdateAsync(int id, Category updatedCategory);
    Task<bool> DeleteAsync(int id);
}