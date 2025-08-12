using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDTO>> GetAllAsync();
        Task<AnnouncementDTO> GetByIdAsync(int id);
        Task<AnnouncementDTO> AddAsync(AnnouncementDTO dto);
        Task<AnnouncementDTO> UpdateAsync(AnnouncementDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

