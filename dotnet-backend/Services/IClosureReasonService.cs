using dotnet_backend.Models;

namespace dotnet_backend.Services
{
    public interface IClosureReasonService
    {
        Task<List<ClosureReason>> GetAllAsync();
        Task<ClosureReason> GetByIdAsync(int id);
        Task<ClosureReason> CreateAsync(ClosureReason closureReason);
        Task<ClosureReason> UpdateAsync(int id, ClosureReason updatedClosureReason);
        Task<bool> DeleteAsync(int id);
    }
}
