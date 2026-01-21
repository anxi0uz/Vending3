using backend.DTOs;

namespace backend.Abstractions
{
    public interface IServicesRepository
    {
        Task<int> CreateServicesAsync(ServiceRequest request);
        Task<int> DeleteServiceAsync(int id);
        Task<List<ServiceResponse>> GetAllServicesAsync();
        Task<int> UpdateServiceAsync(int id, ServiceRequest request);
    }
}