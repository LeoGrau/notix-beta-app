using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;
using Notix.Beta.API.Shared.Persistence.Repositories;

namespace Notix.Beta.API.Notes.Repositories;

public class NoteRepository : BaseRepository<Note, int>, INoteRepository
{
    public NoteRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Note>> ListAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<IEnumerable<Note>> ListByIsArchivedStatusAsync(bool isArchived)
    {
        return await DbSet
            .Where(n => n.IsArchived == isArchived)
            .Include(note => note.NoteCategories)
            .ThenInclude(noteCategory => noteCategory.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Note>> ListByCategoryAsync(int categoryId, bool isArchived)
    {
        return await DbSet
            .Include(note => note.NoteCategories)
            .Where(note => note.NoteCategories.Any(noteCategory => noteCategory.CategoryId == categoryId) &&
                           note.IsArchived == isArchived)
            .ToListAsync();
    }

    public override async Task<Note?> FindAsync(int id)
    {
        return await DbSet
            .Where(n => n.Id == id)
            .Include(note => note.NoteCategories)
            .ThenInclude(noteCategory => noteCategory.Category)
            .FirstOrDefaultAsync();
    }
}