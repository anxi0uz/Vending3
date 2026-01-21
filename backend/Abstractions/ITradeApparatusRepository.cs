using vendingbackend.Core.DTOs;

namespace backend.Abstractions
{
    public interface ITradeApparatusRepository
    {
        Task<int> CreateTradeAsync(TradeApparatusRequest request);
        Task<int> DeleteTradeApparatusAsync(int id);
        Task<List<TradeApparatusResponse>> GetAllTradesAsync();
        Task<TradeApparatusResponse?> GetTradeApparatusByIdAsync(int id);
        Task<int> UpdateTradeApparatusAsync(int id, TradeApparatusRequest request);
    }
}