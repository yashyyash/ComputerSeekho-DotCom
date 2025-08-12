using dotnet_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public interface ICampusLifeRepository
    {
        Task<List<CampusLife>> GetAllAsync();
        Task<CampusLife> GetByIdAsync(int id);
        Task<CampusLife> AddAsync(CampusLife campusLife);
        Task<CampusLife> UpdateAsync(CampusLife campusLife);
        Task<bool> DeleteAsync(int id);
    }
}
