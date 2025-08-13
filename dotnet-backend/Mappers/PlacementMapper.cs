using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class PlacementMapper
    {
        public static PlacementDTO ToPlacementDTO(this Placement placement)
        {
            return new PlacementDTO
            {
                PlacementId = placement.PlacementId,
                BatchId = placement.BatchId,
                CourseId = placement.CourseId,
                RecruiterId = placement.RecruiterId,
                PlacedStudents = placement.PlacedStudents,
                BatchName = placement.Batch?.BatchName,
                CourseName = placement.Course?.CourseName,
                RecruiterName = placement.Recruiter?.CompanyName
            };
        }

        public static Placement ToPlacement(this CreatePlacementDTO dto)
        {
            return new Placement
            {
                BatchId = dto.BatchId,
                CourseId = dto.CourseId,
                RecruiterId = dto.RecruiterId,
                PlacedStudents = dto.PlacedStudents
            };
        }
    }
}
