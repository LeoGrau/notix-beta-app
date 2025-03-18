using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notix.Beta.API.Notes.Domain.Models;

namespace Notix.Beta.API.Shared.Persistence.Configuration.Seeding;

public class NoteSeedingConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasData(new Note
        {
            Id = 1,
            Title = "First Note",
            Content = "This is the content of the first note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        }, new Note
        {
            Id = 2,
            Title = "Second Note",
            Content = "This is the content of the second note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 3,
            Title = "Third Note",
            Content = "This is the content of the third note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 4,
            Title = "Fourth Note",
            Content = "This is the content of the fourth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 5,
            Title = "Fifth Note",
            Content = "This is the content of the fifth note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 6,
            Title = "Sixth Note",
            Content = "This is the content of the sixth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 7,
            Title = "Seventh Note",
            Content = "This is the content of the seventh note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 8,
            Title = "Eighth Note",
            Content = "This is the content of the eighth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 9,
            Title = "Ninth Note",
            Content = "This is the content of the ninth note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 10,
            Title = "Tenth Note",
            Content = "This is the content of the tenth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 11,
            Title = "Eleventh Note",
            Content = "This is the content of the eleventh note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 12,
            Title = "Twelfth Note",
            Content = "This is the content of the twelfth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 13,
            Title = "Thirteenth Note",
            Content = "This is the content of the thirteenth note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 14,
            Title = "Fourteenth Note",
            Content = "This is the content of the fourteenth note.",
            IsArchived = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, new Note
        {
            Id = 15,
            Title = "Fifteenth Note",
            Content = "This is the content of the fifteenth note.",
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
}