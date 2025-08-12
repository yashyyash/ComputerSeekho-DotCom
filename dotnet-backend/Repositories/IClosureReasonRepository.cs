using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IClosureReasonRepository
    {
        Task<List<ClosureReason>> GetAllAsync();
        Task<ClosureReason> GetByIdAsync(int id);
        Task<ClosureReason> AddAsync(ClosureReason closureReason);
        Task<ClosureReason> UpdateAsync(ClosureReason closureReason);
        Task<bool> DeleteAsync(int id);
    }
}

