using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task<Announcement> AddAsync(Announcement announcement);
        Task<Announcement> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(int id);
    }
}

