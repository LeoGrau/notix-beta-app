using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Services.Communication;

namespace Notix.Beta.API.Notes.Domain.Services.Communication;

public class NoteResponse : BaseResponse<Note>
{
    public NoteResponse(Note resource) : base(resource)
    {
    }

    public NoteResponse(string message) : base(message)
    {
    }
}