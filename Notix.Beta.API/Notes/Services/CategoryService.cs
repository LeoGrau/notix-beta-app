using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Notes.Domain.Services;
using Notix.Beta.API.Notes.Domain.Services.Communication;
using Notix.Beta.API.Shared.Domain.Repositories;

namespace Notix.Beta.API.Notes.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryResponse> FindAsync(int id)
    {
        var existingCategory = await _categoryRepository.FindAsync(id);
        if(existingCategory == null)
            return new CategoryResponse("Category not found");
        return new CategoryResponse(existingCategory);
    }

    public async Task<IEnumerable<Category>> ListAllAsync()
    {
        return await _categoryRepository.ListAllAsync();
    }

    public async Task<CategoryResponse> CreateAsync(Category newCategory)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _categoryRepository.AddAsync(newCategory);
            await _unitOfWork.CommitTransactionAsync();
            return new CategoryResponse(newCategory);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new CategoryResponse(e.Message);
        }
    }

    public Task<CategoryResponse> UpdateAsync(int id, Category updatedCategory)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}