namespace Notix.Beta.API.Notes.Domain.Models.Intermediate;

public class NoteCategory
{
    public int NoteId { get; set; }
    public Note Note { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}