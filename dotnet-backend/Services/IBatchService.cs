using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchDto>> GetAllBatchesAsync();
        Task<BatchDto> GetBatchByIdAsync(int id);
        Task<BatchDto> CreateBatchAsync(BatchDto batchDto);
        Task<bool> UpdateBatchAsync(int id, BatchDto batchDto);
        Task<bool> DeleteBatchAsync(int id);
    }
}
