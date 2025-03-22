using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notix.Beta.API.Auth.Domain.Models;

namespace Notix.Beta.API.Shared.Persistence.Configuration.Seeding;

public class UserSeedingConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = 1,
            Email = "admin@notix-beta.org",
            FirstName = "Admin",
            LastName = "Admin",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123")
        }, new User
        {
            Id = 2,
            Email = "admin2@notix-beta.org",
            FirstName = "Admin2",
            LastName = "Admin2",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("admin234")
        }, new User
        {
            Id = 3,
            Email = "leonardo.grau@outlook.com.pe",
            FirstName = "Leonardo Manuel",
            LastName = "Grau Vargas",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword("1234")
        });
    }
}