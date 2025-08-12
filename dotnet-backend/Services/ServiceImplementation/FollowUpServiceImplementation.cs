using dotnet_backend.Models;
using dotnet_backend.Repositories;

namespace dotnet_backend.Services
{
    public class FollowUpServiceImplementation : IFollowUpService
    {
        private readonly IFollowUpRepository _followUpRepository;

        public FollowUpServiceImplementation(IFollowUpRepository followUpRepository)
        {
            _followUpRepository = followUpRepository;
        }

        public async Task<IEnumerable<FollowUp>> GetAllFollowUpsAsync()
        {
            return await _followUpRepository.GetAllAsync();
        }

        public async Task<FollowUp?> GetFollowUpByIdAsync(int id)
        {
            return await _followUpRepository.GetByIdAsync(id);
        }

        public async Task<FollowUp> CreateFollowUpAsync(FollowUp followUp)
        {
            return await _followUpRepository.AddAsync(followUp);
        }

        public async Task<bool> UpdateFollowUpAsync(FollowUp followUp)
        {
            return await _followUpRepository.UpdateAsync(followUp);
        }

        public async Task<bool> DeleteFollowUpAsync(int id)
        {
            return await _followUpRepository.DeleteAsync(id);
        }
    }
}
