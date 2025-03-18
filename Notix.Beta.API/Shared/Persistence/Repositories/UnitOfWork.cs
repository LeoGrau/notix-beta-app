using Microsoft.EntityFrameworkCore.Storage;
using Notix.Beta.API.Shared.Domain.Repositories;
using Notix.Beta.API.Shared.Persistence.Context;

namespace Notix.Beta.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        catch
        {
            await _transaction!.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction!.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction!.RollbackAsync();
            await _transaction!.DisposeAsync();
        }
    }

    // Method to save changes without explicit transaction - NOT SAFE ENOUGH!
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}