using backend.DTOs;

namespace backend.Abstractions
{
    public interface ISalesRepository
    {
        Task<int> CreateSaleAsync(SalesRequest request);
        Task<int> DeleteSaleAsync(int id);
        Task<List<SalesResponse>> GetSalesAsync();
        Task<int> UpdateSaleAsync(int id, SalesRequest request);
    }
}