using dotnet_backend.DTOs;
using dotnet_backend.Models;

namespace dotnet_backend.Mappers
{
    public static class PlacementMapper
    {
        public static PlacementDTO ToDto(Placement placement)
        {
            if (placement == null) return null;

            return new PlacementDTO
            {
                PlacementId = placement.PlacementId,
                BatchId = placement.BatchId,
                CourseId = placement.CourseId,
                RecruiterId = placement.RecruiterId,
                PlacedStudents = placement.PlacedStudents
            };
        }

        public static Placement ToEntity(PlacementCreateDTO dto)
        {
            if (dto == null) return null;

            return new Placement
            {
                BatchId = dto.BatchId,
                CourseId = dto.CourseId,
                RecruiterId = dto.RecruiterId,
                PlacedStudents = dto.PlacedStudents
            };
        }

        public static void UpdateEntity(Placement entity, PlacementUpdateDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.BatchId = dto.BatchId;
            entity.CourseId = dto.CourseId;
            entity.RecruiterId = dto.RecruiterId;
            entity.PlacedStudents = dto.PlacedStudents;
        }
    }
}
