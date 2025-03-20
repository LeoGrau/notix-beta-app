using Microsoft.EntityFrameworkCore;
using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;
using Notix.Beta.API.Shared.Extensions.Builder;
using Notix.Beta.API.Shared.Persistence.Configuration.Seeding;

namespace Notix.Beta.API.Shared.Persistence.Context;

public class AppDbContext : DbContext
{
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<NoteCategory> NoteCategories { get; set; }
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>()
            .HasMany(user => user.Notes)
            .WithOne(note => note.User)
            .HasForeignKey(note => note.UserId);
        
        // Note
        modelBuilder.Entity<Note>().HasKey(n => n.Id);
        
        // Category
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        
        // NoteCategories
        modelBuilder.Entity<NoteCategory>().HasKey(c => new { c.NoteId, c.CategoryId });

        modelBuilder.ApplyConfiguration(new UserSeedingConfiguration());
        modelBuilder.ApplyConfiguration(new NoteSeedingConfiguration());
        modelBuilder.ApplyConfiguration(new CategorySeedingConfiguration());
        modelBuilder.ApplyConfiguration(new NoteCategorySeedingConfiguration());
        modelBuilder.ConvertAllToSnakeCase();
    }
}