using Notix.Beta.API.Notes.Domain.Models.Intermediate;
using Notix.Beta.API.Shared.Domain.Models;

namespace Notix.Beta.API.Notes.Domain.Models;

public class Category : AuditModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<NoteCategory> NoteCategories { get; set; } = new List<NoteCategory>();
}