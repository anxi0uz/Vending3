using backend.DataAccess;
using backend.DTOs;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace backend.Tests
{
    public class ProductTests
    {
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task GetListOfProducts_Products_ListOfProducts()
        {
            using var context = await CreateInMemoryContext();
            var repository = new ProductRepository(context);
            var list = new List<ProductRequest>()
            {
                new ProductRequest("test","test",15,15,0,15),
                new ProductRequest("test","test",15,15,0,15),
                new ProductRequest("test","test",15,15,0,15),
                new ProductRequest("test","test",15,15,0,15),
                new ProductRequest("test","test",15,15,0,15),
            };
            foreach (var item in list)
            {
                await repository.CreateProductAsync(item);
            }

            var result = await repository.GetProductsAsync();
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }
        [Fact]
        public async Task CreateProduct_Product_Int()
        {
            using var context = await CreateInMemoryContext();
            var repository = new ProductRepository(context);
            var request = new ProductRequest("test", "test", 15, 15, 0, 15);

            var result = await repository.CreateProductAsync(request);

            Assert.NotNull(result);
            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task UpdateProduct_Product_Id_Id()
        {
            using var context = await   CreateInMemoryContext();
            var repository = new ProductRepository(context);
            var request = new ProductRequest("test", "test", 15, 15, 0, 15);
            var request2 = new ProductRequest("test2", "test2", 15, 15, 0, 15);
            await repository.CreateProductAsync(request);

            var result = await repository.UpdateProductAsync(1,request2);

            Assert.NotNull(result);
            Assert.NotEqual(0,result);
        }

        [Fact]
        public async Task DeleteProduct_Id_Id()
        {
            using var context = await CreateInMemoryContext();
            var repository = new ProductRepository(context);
            var request = new ProductRequest("test", "test", 15, 15, 0, 15);
            await repository.CreateProductAsync(request);

            var result = await repository.DeleteProductAsync(1);

            Assert.NotNull(result);
            Assert.NotEqual(0, result);
        }
    }
}
