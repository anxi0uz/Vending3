using backend.Abstractions;
using backend.DataAccess;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDbContext dbContext;

        public SalesRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SalesResponse>> GetSalesAsync()
        {
            return await dbContext.Sales
                .AsNoTracking()
                .Select(s => new SalesResponse(s.Id, s.ApparatusId, s.ProductId, s.Quantity, s.TotalPrice, s.SaleDate, s.PayType.ToString()))
                .ToListAsync();
        }

        public async Task<int> CreateSaleAsync(SalesRequest request)
        {
            var model = new Sales()
            {
                ApparatusId = request.apparatusid,
                Quantity = request.quantity,
                ProductId = request.productid,
                TotalPrice = request.totalprice,
                SaleDate = request.saledate,
                PayType = (PayType)request.PayType
            };
            await dbContext.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model.Id;
        }

        public async Task<int> UpdateSaleAsync(int id, SalesRequest request)
        {
            await dbContext.Sales.Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.ProductId, request.productid)
                .SetProperty(s => s.ApparatusId, request.apparatusid)
                .SetProperty(s => s.PayType, (PayType)request.PayType)
                .SetProperty(s => s.Quantity, request.quantity)
                .SetProperty(s => s.SaleDate, request.saledate)
                .SetProperty(s => s.TotalPrice, request.totalprice));
            await dbContext.SaveChangesAsync();
            return id;
        }
        public async Task<int> DeleteSaleAsync(int id)
        {
            await dbContext.Sales.Where(s => s.Id == id).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
