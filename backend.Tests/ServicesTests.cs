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
    public class ServicesTests
    {
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }
        [Fact]
        public async Task GetListOfServices_Services_ListOfServices()
        {
            var context = await CreateInMemoryContext();
            var repository = new ServicesRepository(context);
            var hasher = new PasswordHasher<User>();
            var tarepo = new TradeApparatusRepository(context);
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            await userrepo.CreateUser(user);
            await tarepo.CreateTradeAsync(request);
            var list = new List<ServiceRequest>()
            {
                new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now),"test", "test", 1),
                new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now),"test", "test", 1),
                new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now),"test", "test", 1),
                new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now),"test", "test", 1),
            };

            foreach(var item in list)
            {
                await repository.CreateServicesAsync(item);
            }


            var result = await repository.GetAllServicesAsync();
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Count);
        }
        [Fact]
        public async Task CreateService_Service_Id()
        {
            var context =  await CreateInMemoryContext();
            var repository = new ServicesRepository(context);
            var hasher = new PasswordHasher<User>();
            var tarepo = new TradeApparatusRepository(context);
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var servicereq = new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now), "test", "test", 1);
            await userrepo.CreateUser(user);
            await tarepo.CreateTradeAsync(request);


            var result = await repository.CreateServicesAsync(servicereq);
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateService_Service_Id()
        {
            var context =   await CreateInMemoryContext();
            var repository = new ServicesRepository(context);
            var hasher = new PasswordHasher<User>();
            var tarepo = new TradeApparatusRepository(context);
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var servicereq = new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now), "test", "test", 1);
            var serviceupdatereq = new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now), "test2", "test2", 1);
            await userrepo.CreateUser(user);
            await tarepo.CreateTradeAsync(request);
            await repository.CreateServicesAsync(servicereq);

            var result = await repository.UpdateServiceAsync(1, serviceupdatereq);

            Assert.NotNull(result);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task DeleteService_Id_Id()
        {
            var context =  await CreateInMemoryContext();
            var repository = new ServicesRepository(context);
            var hasher = new PasswordHasher<User>();
            var tarepo = new TradeApparatusRepository(context);
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var servicereq = new ServiceRequest(1, DateOnly.FromDateTime(DateTime.Now), "test", "test", 1);
            await userrepo.CreateUser(user);
            await tarepo.CreateTradeAsync(request);
            await repository.CreateServicesAsync(servicereq);

            var result = await repository.DeleteServiceAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }
    }
}
