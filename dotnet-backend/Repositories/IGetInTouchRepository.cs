using dotnet_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public interface IGetInTouchRepository
    {
        Task<List<GetInTouch>> GetAllAsync();
        Task<GetInTouch> GetByIdAsync(int id);
        Task<GetInTouch> AddAsync(GetInTouch entity);
        Task<GetInTouch> UpdateAsync(GetInTouch entity);
        Task<bool> DeleteAsync(int id);
    }
}
