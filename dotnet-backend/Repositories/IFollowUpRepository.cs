using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IFollowUpRepository
    {
        Task<IEnumerable<FollowUp>> GetAllAsync();
        Task<FollowUp?> GetByIdAsync(int id);
        Task<FollowUp> AddAsync(FollowUp followUp);
        Task<bool> UpdateAsync(FollowUp followUp);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
