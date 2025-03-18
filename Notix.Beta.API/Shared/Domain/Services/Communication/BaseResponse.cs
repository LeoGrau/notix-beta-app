namespace Notix.Beta.API.Shared.Domain.Services.Communication;

public class BaseResponse<TEntity>
{
    public TEntity Resource { get; set; } = default!;
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;

    public BaseResponse(TEntity resource)
    {
        Resource = resource;
        Success = true;
        Message = string.Empty;
    }

    public BaseResponse(string message)
    {
        Resource = default!;
        Message = message;
        Success = false;
    }
}