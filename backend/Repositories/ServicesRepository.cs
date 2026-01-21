using backend.Abstractions;
using backend.DataAccess;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly AppDbContext _context;

        public ServicesRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<ServiceResponse>> GetAllServicesAsync()
        {
            return await _context.Services
                .AsNoTracking()
                .Select(s => new ServiceResponse(s.Id, s.ApparatusId, s.Date, s.Description, s.Problems, s.UserId))
                .ToListAsync();
        }

        public async Task<int> CreateServicesAsync(ServiceRequest request)
        {
            var model = new Services()
            {
                ApparatusId = request.apparatusid,
                Date = request.Date,
                Description = request.description,
                Problems = request.problems,
                UserId = request.UserId,
            };
            await _context.Services.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<int> UpdateServiceAsync(int id, ServiceRequest request)
        {
            await _context.Services
               .Where(s => s.Id == id)
               .ExecuteUpdateAsync(s => s
               .SetProperty(s => s.ApparatusId, request.apparatusid)
               .SetProperty(s => s.Problems, request.problems)
               .SetProperty(s => s.UserId, request.UserId)
               .SetProperty(s => s.Description, request.description)
               .SetProperty(s => s.Date, request.Date));
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> DeleteServiceAsync(int id)
        {
            await _context.Services.Where(t => t.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
