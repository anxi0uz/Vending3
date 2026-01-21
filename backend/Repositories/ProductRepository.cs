using backend.Abstractions;
using backend.DataAccess;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            return await dbContext.Products
                .AsNoTracking()
                .Select(s => new ProductResponse(s.Id, s.Name, s.Description, s.Price, s.Quantity, s.MinimalStock, s.AvgDailySales))
                .ToListAsync();
        }

        public async Task<int> CreateProductAsync(ProductRequest request)
        {
            var model = new Product
            {
                Name = request.name,
                Description = request.description,
                Price = request.price,
                Quantity = request.quantity,
                MinimalStock = request.minimalstock,
                AvgDailySales = request.avgdailysales
            };
            await dbContext.Products.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> UpdateProductAsync(int id, ProductRequest request)
        {
            await dbContext.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.Name, request.name)
                .SetProperty(s => s.Description, request.description)
                .SetProperty(s => s.Price, request.price)
                .SetProperty(s => s.AvgDailySales, request.avgdailysales)
                .SetProperty(s => s.MinimalStock, request.minimalstock));
            await dbContext.SaveChangesAsync();
            return id;
        }
        public async Task<int> DeleteProductAsync(int id)
        {
            await dbContext.Products.Where(s => s.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
