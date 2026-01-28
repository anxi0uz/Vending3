using backend.DataAccess;
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
    public class TradeApparatusesTests
    {
        private async Task<AppDbContext> CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("Host=logiflowadvanced.online;Port=5432;Database=vending34;User Id=postgres;Password=anton123leha").Options;
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }
        [Fact]
        public async Task GetListOfApparatus_Apparatus_ListOfTradeAppparatus()
        {
            var context = await CreateInMemoryContext();
            var repository = new TradeApparatusRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var list = new List<TradeApparatusRequest>()
            {
                 new TradeApparatusRequest("test1",1,15,Guid.NewGuid(),"test",DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now),1),
                 new TradeApparatusRequest("test2",1,15,Guid.NewGuid(),"test",DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now),1),
                 new TradeApparatusRequest("test3",1,15,Guid.NewGuid(),"test",DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now),1),
                 new TradeApparatusRequest("test4",1,15,Guid.NewGuid(),"test",DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),DateOnly.FromDateTime(DateTime.Now),5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now),1)
            };
            await userrepo.CreateUser(user);
            foreach (var ta in list)
            {
                await repository.CreateTradeAsync(ta);
            }

            var result = await repository.GetAllTradesAsync();
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Count);
        }
        [Fact]
        public async Task CreateTradeApparatus_Apparatus_Int()
        {
            var context = await CreateInMemoryContext();
            var repository = new TradeApparatusRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            await userrepo.CreateUser(user);
            var result = await repository.CreateTradeAsync(request);

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateTradeAparatus_IdApparatus_Int()
        {
            var context =  await CreateInMemoryContext();
            var repository = new TradeApparatusRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            var request2 = new TradeApparatusRequest("test2", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            await userrepo.CreateUser(user);
            await repository.CreateTradeAsync(request);

            var result = await repository.UpdateTradeApparatusAsync(1,request);

            Assert.NotNull(result);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task DeleteTradeApparatus_Id_Int()
        {
            var context = await CreateInMemoryContext();
            var repository = new TradeApparatusRepository(context);
            var hasher = new PasswordHasher<User>();
            var userrepo = new UserRepository(context, hasher);
            var user = new UserRequest("test", "testov test", "test", 1);
            var request = new TradeApparatusRequest("test1", 1, 15, Guid.NewGuid(), "test", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 5, 15, DateOnly.FromDateTime(DateTime.Now), 15, 1, "test", DateOnly.FromDateTime(DateTime.Now), 1);
            await userrepo.CreateUser(user);
            await repository.CreateTradeAsync(request);

            var result = await repository.DeleteTradeApparatusAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result);
        }
    }
}
