using backend.Abstractions;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/products").RequireAuthorization();

            group.MapGet("/", async (IProductRepository repository) =>
            {
                return await repository.GetProductsAsync();
            });

            group.MapPost("/", async (IProductRepository repository, [FromBody]ProductRequest request) =>
            {
                return await repository.CreateProductAsync(request);
            });

            group.MapPut("/{id}", async (IProductRepository repository, int id, [FromBody]ProductRequest request) =>
            {
                return await repository.UpdateProductAsync(id, request);
            });

            group.MapDelete("/{id}", async (IProductRepository repository, int id) =>
            {
                return await repository.DeleteProductAsync(id);
            });

            return group;
        }
    }
}
