using dotnet_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAllAsync();
        Task<Faculty> GetByIdAsync(int id);
        Task<Faculty> AddAsync(Faculty faculty);
        Task<Faculty> UpdateAsync(Faculty faculty);
        Task<bool> DeleteAsync(int id);
    }
}
