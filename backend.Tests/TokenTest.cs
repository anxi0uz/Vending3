using backend.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendingbackend.Core.DTOs;

namespace backend.Tests
{
    public class TokenTest
    {
        [Fact]
        public async Task GenerateAccessToken_UserResponse_JwtToken()
        {
            var config = CreateConfiguration();
            var service = new TokenService(config);
            var userResponse = new UserResponse(1,"test","test","admin","testov test testovich");

            var token = service.GenerateAccessToken(userResponse);

            Assert.NotNull(token);
        }

        [Fact]
        public async Task GenerateRefreshToken_GenerateToken()
        {
            var config = CreateConfiguration();
            var service = new TokenService(config);

            var response = service.GenerateRefreshToken();

            Assert.NotNull(response);
        }

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
    }
}
