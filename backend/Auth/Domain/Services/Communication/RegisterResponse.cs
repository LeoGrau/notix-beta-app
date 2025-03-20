namespace Notix.Beta.API.Auth.Domain.Services.Communication;

public class RegisterResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public int UserId { get; set; }
    
    public RegisterResponse(string message)
    {
        Message = message;
        Success = false;
    }
    
    public RegisterResponse(int userId)
    {
        UserId = userId;
        Message = string.Empty;
        Success = true;
    }
}