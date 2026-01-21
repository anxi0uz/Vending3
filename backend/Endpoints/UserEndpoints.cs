using backend.Abstractions;
using Microsoft.AspNetCore.Mvc;
using vendingbackend.Core.DTOs;

namespace backend.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/user");
            group.MapPost("/login", async (IAuthService service, [FromBody] AuthRequest request) =>
            {
                return await service.LoginAsync(request);
            });
            group.MapPost("/register", async (IAuthService service, [FromBody] UserRequest request) =>
            {
                return await service.Register(request);
            });
            group.MapGet("/", async () =>
            {

            });
            return group;
        }
    }
}
