using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Services.Communication;

namespace Notix.Beta.API.Notes.Domain.Services;

public interface INoteService
{
    Task<NoteResponse> FindAsync(int id);
    Task<IEnumerable<Note>> ListByIsArchivedStatusAsync(bool isArchived);
    Task<IEnumerable<Note>> ListAllAsync();
    Task<IEnumerable<Note>> ListByCategoryAndIsArchivedAsync(int categoryId, bool isArchived);
    Task<NoteResponse> CreateAsync(Note newNote);
    Task<NoteResponse> UpdateAsync(int id, Note updatedNote);
    Task<NoteResponse> SetIsArchiveStatusAsync(int id, bool isArchive);
    Task<NoteResponse> DeleteAsync(int id);
}