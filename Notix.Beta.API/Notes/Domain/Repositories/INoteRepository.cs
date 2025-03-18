using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Notes.Domain.Repositories;

public interface INoteRepository : IBaseRepository<Note, int>
{
    Task<IEnumerable<Note>> ListAllAsync();
    Task<IEnumerable<Note>> ListByIsArchivedStatusAsync(bool isArchived);
    Task<IEnumerable<Note>> ListByCategoryAsync(int categoryId, bool isArchived);
}