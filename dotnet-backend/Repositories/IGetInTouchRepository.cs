using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IGetInTouchRepository
    {
        Task<IEnumerable<GetInTouch>> GetAllAsync();
        Task<GetInTouch> GetByIdAsync(int id);
        Task<GetInTouch> AddAsync(GetInTouch contact);
        Task<bool> DeleteAsync(int id);
    }
}
