using backend.Abstractions;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Endpoints
{
    public static class SalesEndpoints
    {
        public static IEndpointRouteBuilder MapSalesEndpoinst(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/sales").RequireAuthorization();

            group.MapGet("/", async (ISalesRepository repository) =>
            {
                return await repository.GetSalesAsync();
            });

            group.MapPost("/", async (ISalesRepository repository,[FromBody] SalesRequest request) =>
            {
                return await repository.CreateSaleAsync(request);
            });

            group.MapPut("/{id}", async (ISalesRepository repository, int id, [FromBody] SalesRequest request) =>
            {
                return await repository.UpdateSaleAsync(id, request);
            });

            group.MapDelete("/{id}", async (ISalesRepository repository, int id) =>
            {
                return await repository.DeleteSaleAsync(id);
            });

            return group;
        }
    }
}
