using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Shared.Domain.Services.Communication;

namespace Notix.Beta.API.Notes.Domain.Services.Communication;

public class CategoryResponse : BaseResponse<Category>
{
    public CategoryResponse(Category resource) : base(resource)
    {
    }

    public CategoryResponse(string message) : base(message)
    {
    }
}