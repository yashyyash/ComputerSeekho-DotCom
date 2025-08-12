using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface IAnnouncementService
    {
        Task<List<AnnouncementDto>> GetAllAnnouncementsAsync();
        Task<AnnouncementDto> GetAnnouncementByIdAsync(int id);
        Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto dto);
        Task<AnnouncementDto> UpdateAnnouncementAsync(int id, AnnouncementDto dto);
        Task<bool> DeleteAnnouncementAsync(int id);
    }
}
