using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notix.Beta.API.Notes.Domain.Models;

namespace Notix.Beta.API.Shared.Persistence.Configuration.Seeding;

public class CategorySeedingConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category { Id = 1, Name = "Work" }, new Category { Id = 2, Name = "Personal" },
            new Category { Id = 3, Name = "Shopping" }, new Category { Id = 4, Name = "Health" },
            new Category { Id = 5, Name = "Finance" }, new Category { Id = 6, Name = "Education" },
            new Category { Id = 7, Name = "Fitness" }, new Category { Id = 8, Name = "Travel" },
            new Category { Id = 9, Name = "Hobbies" }, new Category { Id = 10, Name = "Goals" },
            new Category { Id = 11, Name = "Entertainment" }, new Category { Id = 12, Name = "Family" },
            new Category { Id = 13, Name = "Friends" }, new Category { Id = 14, Name = "Projects" },
            new Category { Id = 15, Name = "Miscellaneous" });
    }
}