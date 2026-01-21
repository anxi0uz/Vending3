using backend.DTOs;

namespace backend.Abstractions
{
    public interface IProductRepository
    {
        Task<int> CreateProductAsync(ProductRequest request);
        Task<int> DeleteProductAsync(int id);
        Task<List<ProductResponse>> GetProductsAsync();
        Task<int> UpdateProductAsync(int id, ProductRequest request);
    }
}