//using dotnet_backend.DTOs;
//using dotnet_backend.Models;

//namespace dotnet_backend.Mappers
//{
//    public static class PlacementMapper
//    {
//        public static PlacementDTO ToDto(Placement placement)
//        {
//            if (placement == null) return null;

//            return new PlacementDTO
//            {
//                PlacementId = placement.PlacementId,
//                BatchId = placement.BatchId,
//                CourseId = placement.CourseId,
//                RecruiterId = placement.RecruiterId,
//                PlacedStudents = placement.PlacedStudents
//            };
//        }

//        public static Placement ToEntity(PlacementCreateDTO dto)
//        {
//            if (dto == null) return null;

//            return new Placement
//            {
//                BatchId = dto.BatchId,
//                CourseId = dto.CourseId,
//                RecruiterId = dto.RecruiterId,
//                PlacedStudents = dto.PlacedStudents
//            };
//        }

//        public static void UpdateEntity(Placement entity, PlacementUpdateDTO dto)
//        {
//            if (entity == null || dto == null) return;

//            entity.BatchId = dto.BatchId;
//            entity.CourseId = dto.CourseId;
//            entity.RecruiterId = dto.RecruiterId;
//            entity.PlacedStudents = dto.PlacedStudents;
//        }
//    }
//}

using dotnet_backend.DTOs;
using dotnet_backend.Models;

namespace dotnet_backend.Mappers
{
    public static class PlacementMapper
    {
        // Entity -> DTO
        public static PlacementDto ToDto(Placement placement)
        {
            if (placement == null) return null;

            // First create DTO object
            var dto = new PlacementDto
            {
                PlacementId = placement.PlacementId,
                BatchId = placement.BatchId,
                CourseId = placement.CourseId,
                RecruiterId = placement.RecruiterId,
                StudentId = placement.StudentId
            };

            // Then set optional display fields
            dto.StudentName = placement.Student?.StudentName;
            dto.StudentPhotoUrl = placement.Student?.StudentPhotoUrl;
            dto.BatchName = placement.Batch?.BatchName;
            dto.CompanyName = placement.Course?.CourseName; // or placement.Recruiter?.CompanyName if needed
            dto.RecuriterName = placement.Recruiter?.RecruiterPhotoUrl;

            // Return the DTO object
            return dto;
        }

        // DTO -> Entity
        public static Placement ToEntity(PlacementDto dto)
        {
            if (dto == null) return null;

            // Remove PlacementId assignment
            var entity = new Placement
            {
                BatchId = dto.BatchId,
                CourseId = dto.CourseId,
                RecruiterId = dto.RecruiterId,
                StudentId = dto.StudentId
            };

            return entity;
        }

    }
}
