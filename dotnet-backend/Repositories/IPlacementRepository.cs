using dotnet_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public interface IPlacementRepository
    {
        Task<IEnumerable<Placement>> GetAllAsync();
        Task<IEnumerable<Placement>> GetByBatchIdAsync(int batchId);
        Task<Placement> GetByIdAsync(int id);
        Task AddAsync(Placement placement);
        Task UpdateAsync(Placement placement);
        Task<bool> DeleteAsync(Placement placement);
    }
}
