namespace Notix.Beta.API.Notes.Resources.Update;

public class UpdateNoteResource
{
    public int UserId { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
    public List<int> NoteCategoriesId { get; set; } = new();
}