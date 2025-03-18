using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;

namespace Notix.Beta.API.Shared.Persistence.Configuration.Seeding;

public class NoteCategorySeedingConfiguration : IEntityTypeConfiguration<NoteCategory>
{
    public void Configure(EntityTypeBuilder<NoteCategory> builder)
    {
        builder.HasData(
            new NoteCategory { NoteId = 1, CategoryId = 1 }, // Work
            new NoteCategory { NoteId = 1, CategoryId = 14 }, // Projects
            new NoteCategory { NoteId = 2, CategoryId = 2 }, // Personal
            new NoteCategory { NoteId = 3, CategoryId = 3 }, // Shopping
            new NoteCategory { NoteId = 4, CategoryId = 4 }, // Health
            new NoteCategory { NoteId = 5, CategoryId = 5 }, // Finance
            new NoteCategory { NoteId = 6, CategoryId = 6 }, // Education
            new NoteCategory { NoteId = 7, CategoryId = 7 }, // Fitness
            new NoteCategory { NoteId = 8, CategoryId = 8 }, // Travel
            new NoteCategory { NoteId = 9, CategoryId = 9 }, // Hobbies
            new NoteCategory { NoteId = 10, CategoryId = 10 }, // Goals
            new NoteCategory { NoteId = 11, CategoryId = 11 }, // Entertainment
            new NoteCategory { NoteId = 12, CategoryId = 12 }, // Family
            new NoteCategory { NoteId = 13, CategoryId = 13 }, // Friends
            new NoteCategory { NoteId = 14, CategoryId = 14 }, // Projects
            new NoteCategory { NoteId = 15, CategoryId = 15 } // Miscellaneous
        );
    }
}