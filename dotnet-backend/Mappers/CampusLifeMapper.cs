using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class CampusLifeMapper
    {
        public static CampusLifeDto ToDto(CampusLife entity)
        {
            if (entity == null) return null;

            return new CampusLifeDto
            {
                CampusLifeId = entity.CampusLifeId,
                PhotoUrl = entity.PhotoUrl,
                Description = entity.Description
            };
        }

        public static CampusLife ToEntity(CampusLifeDto dto)
        {
            if (dto == null) return null;

            return new CampusLife
            {
                // Do NOT set CampusLifeId - it's auto-incremented by DB
                PhotoUrl = dto.PhotoUrl,
                Description = dto.Description
            };
        }
    }
}
