namespace Notix.Beta.API.Notes.Resources.Update.Patch;

public class PatchNoteResource
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsArchived { get; set; }
}