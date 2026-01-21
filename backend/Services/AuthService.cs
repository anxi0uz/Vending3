using backend.Abstractions;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using vendingbackend.Core.DTOs;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> hasher;

        public AuthService(ITokenService tokenService, IUserRepository userRepository, IPasswordHasher<User> hasher)
        {
            this.tokenService = tokenService;
            this.userRepository = userRepository;
            this.hasher = hasher;
        }

        public async Task<int> Register(UserRequest request)
        {
            var user = new User();
            var requestDto = new UserRequest(request.email, request.fio, request.password, request.role);
            return await userRepository.CreateUser(requestDto);
        }


        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var user = await userRepository.GetUserById(request.email);
            if (user == null)
                throw new ArgumentException("Invalid user");
            if (hasher.VerifyHashedPassword(null, user.passwordhash, request.password) == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");
            var token = tokenService.GenerateAccessToken(user);
            return new AuthResponse(token);
        }
    }
}
