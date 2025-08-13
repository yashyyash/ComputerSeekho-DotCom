using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IPlacementRepository
    {
        Task<IEnumerable<Placement>> GetAllAsync();
        Task<Placement> GetByIdAsync(int id);
        Task<Placement> AddAsync(Placement placement);
        Task UpdateAsync(Placement placement);
        Task DeleteAsync(Placement placement);
    }
}
