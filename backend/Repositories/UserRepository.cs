using backend.Abstractions;
using backend.DataAccess;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vendingbackend.Core.DTOs;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<User> hasher;

        public UserRepository(AppDbContext dbContext, IPasswordHasher<User> hasher)
        {
            _dbContext = dbContext;
            this.hasher = hasher;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Select(u => new UserResponse(u.Id, u.Email, u.PasswordHash, u.role.ToString(), u.Fio))
                .ToListAsync();
        }

        public async Task<int> CreateUser(UserRequest request)
        {
            var user = new User { Email = request.email, Fio = request.fio, PasswordHash = hasher.HashPassword(null, request.password) };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }
        public async Task<UserResponse> GetUserById(string email)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Email == email)
                .Select(u => new UserResponse(u.Id, u.Email, u.PasswordHash, u.role.ToString(), u.Fio))
                .FirstOrDefaultAsync();
        }
        public async Task<int> UpdateUser(int id, UserRequest request)
        {
            await _dbContext.Users
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Email, request.email)
                .SetProperty(s => s.role, (Role)request.role)
                .SetProperty(s => s.PasswordHash, hasher.HashPassword(null, request.password)));
            await _dbContext.SaveChangesAsync();

            return id;
        }
        public async Task<int> DeleteUser(int id)
        {
            await _dbContext
                .Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
