using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Notes.Domain.Models.Intermediate;
using Notix.Beta.API.Shared.Domain.Models;

namespace Notix.Beta.API.Notes.Domain.Models;

public class Note : AuditModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public List<NoteCategory> NoteCategories { get; set; } = new();
    
    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; }
}