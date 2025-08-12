using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class AnnouncementMapper
    {
        public static AnnouncementDto ToDto(Announcement announcement)
        {
            if (announcement == null) return null;

            return new AnnouncementDto
            {
                AnnouncementId = announcement.AnnouncementId,
                AnnouncementText = announcement.AnnouncementText
            };
        }

        public static Announcement ToEntity(AnnouncementDto dto)
        {
            if (dto == null) return null;

            return new Announcement
            {
                
                AnnouncementText = dto.AnnouncementText
            };
        }
    }
}
