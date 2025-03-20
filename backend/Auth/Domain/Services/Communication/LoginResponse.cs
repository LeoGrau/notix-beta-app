namespace Notix.Beta.API.Auth.Domain.Services.Communication;

public class LoginResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    
    public LoginResponse(string message)
    {
        Message = message;
        Success = false;
        Token = string.Empty;
    }
    
    public LoginResponse(int userId, string token, int expiresIn)
    {
        UserId = userId;
        Message = string.Empty;
        Success = true;
        Token = token;
        ExpiresIn = expiresIn;
    }
    
}