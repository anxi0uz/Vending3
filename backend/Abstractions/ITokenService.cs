using backend.Models;
using vendingbackend.Core.DTOs;

namespace backend.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserResponse user);
        string GenerateRefreshToken();
    }
}