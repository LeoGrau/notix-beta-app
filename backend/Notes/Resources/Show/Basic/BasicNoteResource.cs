namespace Notix.Beta.API.Notes.Resources.Show.Basic;

public class BasicNoteResource
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsArchived { get; set; }
}