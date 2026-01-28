using backend.DataAccess;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using vendingbackend.Core.DTOs;

namespace backend.Tests
{
    public class SalesTests
    {
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task GetListOfSales_Sales_ListOfSales()
        {
            using var context = await CreateInMemoryContext();   
            var repository = new SalesRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepository = new UserRepository(context,hasher);
            var tarepository = new TradeApparatusRepository(context);
            var product = new ProductRequest("test","test",15,2,0,15);
            var user = new UserRequest("test","testov test","test",1);
            var trademachine = new TradeApparatusRequest("test",1,15,Guid.NewGuid(),"test",DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now),1);
            var salerequest = new SalesRequest(0,0,15,product.price*15,DateTime.Now,1);


            await userrepository.CreateUser(user);
            await tarepository.CreateTradeAsync(trademachine);
            await repository.CreateSaleAsync(salerequest);

            var result = await repository.GetSalesAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateSale_Sales_Id()
        {
            using var context = await CreateInMemoryContext();
            var repository = new SalesRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepository = new UserRepository(context, hasher);
            var tarepository = new TradeApparatusRepository(context);
            var product = new ProductRequest("test", "test", 15, 2, 0, 15);
            var user = new UserRequest("test", "testov test", "test", 1);
            var trademachine = new TradeApparatusRequest("test", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var salerequest = new SalesRequest(0, 0, 15, product.price * 15, DateTime.Now, 1);


            await userrepository.CreateUser(user);
            await tarepository.CreateTradeAsync(trademachine);
            var result = await repository.CreateSaleAsync(salerequest);

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateSale_Sales_Id()
        {
            using var context =     await CreateInMemoryContext();
            var repository = new SalesRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepository = new UserRepository(context, hasher);
            var tarepository = new TradeApparatusRepository(context);
            var product = new ProductRequest("test", "test", 15, 2, 0, 15);
            var user = new UserRequest("test", "testov test", "test", 1);
            var trademachine = new TradeApparatusRequest("test", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var salerequest = new SalesRequest(0, 0, 15, product.price * 15, DateTime.Now, 1);
            var salerequest2 = new SalesRequest(0, 0, 158, product.price * 15, DateTime.Now, 1);


            await userrepository.CreateUser(user);
            await tarepository.CreateTradeAsync(trademachine);
            await repository.CreateSaleAsync(salerequest);
            var result = await repository.UpdateSaleAsync(0, salerequest);

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteSale_Sales_Id()
        {
            using var context = await CreateInMemoryContext();
            var repository = new SalesRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepository = new UserRepository(context, hasher);
            var tarepository = new TradeApparatusRepository(context);
            var product = new ProductRequest("test", "test", 15, 2, 0, 15);
            var user = new UserRequest("test", "testov test", "test", 1);
            var trademachine = new TradeApparatusRequest("test", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var salerequest = new SalesRequest(0, 0, 15, product.price * 15, DateTime.Now, 1);
            var salerequest2 = new SalesRequest(0, 0, 158, product.price * 15, DateTime.Now, 1);


            await userrepository.CreateUser(user);
            await tarepository.CreateTradeAsync(trademachine);
            await repository.CreateSaleAsync(salerequest);
            var result = await repository.DeleteSaleAsync(0);
            var list = await repository.GetSalesAsync();

            Assert.Null(list);
            Assert.NotNull(result);
        }
    }
}
