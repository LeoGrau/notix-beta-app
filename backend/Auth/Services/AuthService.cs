using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Notix.Beta.API.Auth.Domain.Models;
using Notix.Beta.API.Auth.Domain.Repositories;
using Notix.Beta.API.Auth.Domain.Services;
using Notix.Beta.API.Auth.Domain.Services.Communication;
using Notix.Beta.API.Shared.Domain.Repositories;
using RegisterRequest = Notix.Beta.API.Auth.Resources.Auth.RegisterRequest;

namespace Notix.Beta.API.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        var existingUser = await _userRepository.FindUserByUsernameAsync(email);
        if (existingUser is null || !(existingUser.Email == email &&
                                     BCrypt.Net.BCrypt.Verify(password, existingUser.HashedPassword)))
            return new LoginResponse("Email or password are incorrect");

        var token = _jwtService.GenerateToken(existingUser);
        
        var tokenExpiration = int.Parse(_configuration["JwtSettings:TokenExpiration"]!) * 3600;

        return new LoginResponse(existingUser.Id, token, tokenExpiration);
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var newUser = _mapper.Map<User>(request);
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            // Confirm password.
            if (request.Password != request.ConfirmPassword)
                return new RegisterResponse("Passwords do not match");
            
            // Hash Password.
            newUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitTransactionAsync();
            return new RegisterResponse(newUser.Id);
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new RegisterResponse(e.Message);
        }
    }
}