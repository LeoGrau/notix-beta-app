namespace Notix.Beta.API.Notes.Resources.Create;

public class CreateNoteResource
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public List<int> NoteCategoriesId { get; set; } = new();
}