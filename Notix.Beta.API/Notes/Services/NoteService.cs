using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Notes.Domain.Services;
using Notix.Beta.API.Notes.Domain.Services.Communication;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NoteService(INoteRepository noteRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<NoteResponse> FindAsync(int id)
    {
        var existingNote = await _noteRepository.FindAsync(id);
        if (existingNote is null)
            return new NoteResponse("Note is not found");
        return new NoteResponse(existingNote);
    }

    public async Task<IEnumerable<Note>> ListByIsArchivedStatusAsync(bool isArchived)
    {
        return await _noteRepository.ListByIsArchivedStatusAsync(isArchived);
    }

    public async Task<IEnumerable<Note>> ListAllAsync()
    {
        return await _noteRepository.ListAllAsync();
    }

    public async Task<IEnumerable<Note>> ListByCategoryAndIsArchivedAsync(int categoryId, bool isArchived)
    {
        var existingCategory = await _categoryRepository.FindAsync(categoryId);
        if(existingCategory is null)
            throw new Exception("Category is not found");
        return await _noteRepository.ListByCategoryAsync(categoryId, isArchived);
    }

    public async Task<NoteResponse> CreateAsync(Note newNote)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _noteRepository.AddAsync(newNote);

            var categoryIds = newNote.NoteCategories.Select(c => c.CategoryId);
            var categoryDictionary = await _categoryRepository.ListByMutipleIdAsync(categoryIds);

            foreach (var noteCategory in newNote.NoteCategories)
            {
                if (categoryDictionary.TryGetValue(noteCategory.CategoryId, out var category))
                {
                    noteCategory.Category = category;
                }
            }

            await _unitOfWork.CommitTransactionAsync();
            return new NoteResponse(newNote);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new NoteResponse(e.Message);
        }
    }

    public async Task<NoteResponse> UpdateAsync(int id, Note updatedNote)
    {
        var existingNote = await _noteRepository.FindAsync(id);
        if (existingNote is null)
            return new NoteResponse("Note is not found");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;
            existingNote.IsArchived = updatedNote.IsArchived;
            existingNote.UpdatedAt = DateTime.Now;

            var categoryIds = updatedNote.NoteCategories.Select(c => c.CategoryId);
            var categoryDictionary = await _categoryRepository.ListByMutipleIdAsync(categoryIds);

            foreach (var noteCategory in updatedNote.NoteCategories)
            {
                if (categoryDictionary.TryGetValue(noteCategory.CategoryId, out var category))
                {
                    noteCategory.Category = category;
                }
            }

            existingNote.NoteCategories = updatedNote.NoteCategories;

            await _unitOfWork.CommitTransactionAsync();

            return new NoteResponse(existingNote);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new NoteResponse(e.Message);
        }
    }

    public async Task<NoteResponse> SetIsArchiveStatusAsync(int id, bool isArchive)
    {
        var existingNote = await _noteRepository.FindAsync(id);
        if (existingNote is null)
            return new NoteResponse("Note is not found");
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            existingNote.IsArchived = isArchive;
            await _unitOfWork.CommitTransactionAsync();
            return new NoteResponse(existingNote);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new NoteResponse(e.Message);
        }
    }

    public async Task<NoteResponse> DeleteAsync(int id)
    {
        var existingNote = await _noteRepository.FindAsync(id);
        if (existingNote is null)
            return new NoteResponse("Note is not found");
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            _noteRepository.Remove(existingNote);
            await _unitOfWork.CommitTransactionAsync();
            return new NoteResponse(existingNote);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new NoteResponse(e.Message);
        }
    }
}