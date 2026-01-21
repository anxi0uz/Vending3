using vendingbackend.Core.DTOs;

namespace backend.Abstractions
{
    public interface IUserRepository
    {
        Task<int> CreateUser(UserRequest request);
        Task<int> DeleteUser(int id);
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserById(string email);
        Task<int> UpdateUser(int id, UserRequest request);
    }
}