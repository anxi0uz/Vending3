using vendingbackend.Core.DTOs;

namespace backend.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<int> Register(UserRequest request);
    }
}