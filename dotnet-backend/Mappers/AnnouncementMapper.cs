using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class AnnouncementMapper
    {
        public static AnnouncementDTO ToDTO(Announcement announcement)
        {
            return new AnnouncementDTO
            {
                AnnouncementId = announcement.AnnouncementId,
                AnnouncementText = announcement.AnnouncementText
            };
        }

        public static Announcement ToEntity(AnnouncementDTO dto)
        {
            return new Announcement
            {
                AnnouncementId = dto.AnnouncementId,
                AnnouncementText = dto.AnnouncementText
            };
        }
    }
}

