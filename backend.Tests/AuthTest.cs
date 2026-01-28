using backend.DataAccess;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using vendingbackend.Core.DTOs;

namespace backend.Tests
{
    public class AuthTest
    {
        private IConfiguration CreateConfiguration()
        {
            var settings = new Dictionary<string, string>
            {
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"},
                {"Jwt:Key", "SuperSecretKey12345678901234567890"},
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task AuthLogin_AuthRequest_AuthResponse()
        {
            var context = await CreateInMemoryContext();
            var configuration = CreateConfiguration();
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);
            var tokenService = new TokenService(configuration);
            var authService = new AuthService(tokenService, repository, hasher);
            var authRequest = new AuthRequest("test", "test");
            var userRequest = new UserRequest("test", "test", "test", 1);
            await repository.CreateUser(userRequest);

            var response = await authService.LoginAsync(authRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task AuthRegister_UserRequest_Id()
        {
            var context = await  CreateInMemoryContext();
            var configuration = CreateConfiguration();
            var hasher = new PasswordHasher<User>();
            var repository = new UserRepository(context, hasher);
            var tokenService = new TokenService(configuration);
            var authService = new AuthService(tokenService, repository, hasher);
            var userRequest = new UserRequest("test", "test", "test", 1);

            var result = await authService.Register(userRequest);

            Assert.NotNull(result);
            Assert.NotEqual(0, result);
        }
    }
}
