using dotnet_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<List<Announcement>> GetAllAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task<Announcement> AddAsync(Announcement announcement);
        Task<Announcement> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(int id);
    }
}
