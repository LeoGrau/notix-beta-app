namespace Notix.Beta.API.Notes.Resources.Show.Detailed;

public class DetailedNoteResource
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public List<DetailedNoteCategoryResource> NoteCategories { get; set; } = new();
}