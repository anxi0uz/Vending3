using backend.Abstractions;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Endpoints
{
    public static class ServiceEndpoints
    {
        public static IEndpointRouteBuilder MapServiceEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/services").RequireAuthorization();

            group.MapGet("/", async (IServicesRepository repository) =>
            {
                return await repository.GetAllServicesAsync();
            });

            group.MapPost("/", async (IServicesRepository repository, [FromBody] ServiceRequest request) =>
            {
                return await repository.CreateServicesAsync(request);
            });

            group.MapPut("/{id}", async (IServicesRepository repository, int id, [FromBody] ServiceRequest request) =>
            {
                return await repository.UpdateServiceAsync(id, request);
            });

            group.MapDelete("/{id}", async (IServicesRepository repository, int id) =>
            {
                return repository.DeleteServiceAsync(id);

            });
            return group;
        }
    }
}
