using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;
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

    public async Task<IEnumerable<Note>> ListByUserIdAsync(int userId)
    {
        return await DbSet.Where(x => x.UserId == userId).ToListAsync();
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

    public async Task<IEnumerable<Note>> ListNotesByUserIdAndIsActiveStatusAsync(bool isArchived, int userId)
    {
        return await DbSet
            .Include(note => note.NoteCategories)
            .Where(note => note.UserId == userId && note.IsArchived == isArchived)
            .ToListAsync();
    }

    public async Task<IEnumerable<Note>> ListByCategoryAndIsArchivedAndUserIdAsync(int categoryId, bool isArchived,
        int userId)
    {
        return await DbSet
            .Include(note => note.NoteCategories)
            .Where(note => note.UserId == userId && note.IsArchived == isArchived &&
                           note.NoteCategories.Any(noteCategory => noteCategory.CategoryId == categoryId))
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