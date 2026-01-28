using backend.Abstractions;
using backend.DataAccess;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using vendingbackend.Core.DTOs;

namespace backend.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task GetListOfUsers_User_ListOfUsers()
        {
            using var context = await CreateInMemoryContext();
            var expectedUsers = new List<UserRequest>() { new UserRequest("test", "testov test testovich", "test", 1), new UserRequest("test", "testov test testovich", "test", 1), new UserRequest("test", "testov test testovich", "test", 1) };

            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);

            foreach (var user in expectedUsers)
            {
                await repository.CreateUser(user);
            }

            var result = await repository.GetAllUsersAsync();

            Assert.NotNull(result);
        }
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task GetUserByIdAsync_User_UserById()
        {
            using var context = await CreateInMemoryContext();
            var request = new UserRequest("test","testov test","testpasword",1);
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);


            await repository.CreateUser(request);


            var result = await repository.GetUserById("test");

            Assert.NotNull(result);
            Assert.Equal("test", result.email);
        }

        [Fact]
        public async Task UpdateUser_User_User()
        {
            using var context = await  CreateInMemoryContext();

            var request = new UserRequest("test", "testov test", "testpasword", 1);
            var request2 = new UserRequest("test2", "testov tes2t", "testpasword2", 2);
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);

            await repository.CreateUser(request);
            await repository.UpdateUser(1, request2);

            var result = await repository.GetUserById("test2");

            Assert.NotNull(result);
            Assert.Equal("test2", result.email);
        }

        [Fact]
        public async Task DeleteUser_User_Null()
        {
            using var context = await CreateInMemoryContext();
            var request = new UserRequest("test", "testov test", "testpasword", 1);
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);

            await repository.CreateUser(request);
            await repository.DeleteUser(1);

            var result = await repository.GetUserById("test");

            Assert.Null(result);
        }
        [Fact]
        public async Task CreateUser_UserRequest_Id()
        {
            using var context =  await CreateInMemoryContext();
            var request = new UserRequest("test", "testov test", "testpasword", 1);
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);

            var id = await repository.CreateUser(request);
            var result = await repository.GetUserById("test");

            Assert.NotNull(result);
            Assert.Equal(1,id);
        }
    }
}
