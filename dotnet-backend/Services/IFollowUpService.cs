using dotnet_backend.Models;

namespace dotnet_backend.Services
{
    public interface IFollowUpService
    {
        Task<IEnumerable<FollowUp>> GetAllFollowUpsAsync();
        Task<FollowUp?> GetFollowUpByIdAsync(int id);
        Task<FollowUp> CreateFollowUpAsync(FollowUp followUp);
        Task<bool> UpdateFollowUpAsync(FollowUp followUp);
        Task<bool> DeleteFollowUpAsync(int id);
    }
}
