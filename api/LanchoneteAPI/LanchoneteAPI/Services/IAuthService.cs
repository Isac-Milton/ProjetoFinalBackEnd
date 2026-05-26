using LanchoneteAPI.DTOs;

namespace LanchoneteAPI.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}