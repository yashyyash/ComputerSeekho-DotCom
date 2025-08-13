using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IBatchRepository
    {
        Task<IEnumerable<Batch>> GetAllAsync();
        Task<Batch> GetByIdAsync(int id);
        Task AddAsync(Batch batch);
        Task UpdateAsync(Batch batch);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
