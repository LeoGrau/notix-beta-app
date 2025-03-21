using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Notes.Domain.Repositories;

public interface INoteRepository : IBaseRepository<Note, int>
{
    Task<IEnumerable<Note>> ListAllAsync();
    Task<IEnumerable<Note>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Note>> ListByIsArchivedStatusAsync(bool isArchived);
    Task<IEnumerable<Note>> ListByCategoryAsync(int categoryId, bool isArchived);
    Task<IEnumerable<Note>> ListNotesByUserIdAndIsActiveStatusAsync(bool isArchived, int userId);
    Task<IEnumerable<Note>> ListByCategoryAndIsArchivedAndUserIdAsync(int categoryId, bool isArchived, int userId);
    
}