using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IPlacementRepository
    {
        Task<IEnumerable<Placement>> GetAllAsync();
        Task<Placement> GetByIdAsync(int id);
        Task AddAsync(Placement placement);
        Task UpdateAsync(Placement placement);
        Task<bool> DeleteAsync(Placement placement);
    }
}
