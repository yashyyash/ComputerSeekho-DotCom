using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class BatchMapper
    {
        public static BatchDto ToDto(Batch batch)
        {
            if (batch == null) return null;

            var batchDto = new BatchDto
            {
                BatchId = batch.BatchId,
                BatchName = batch.BatchName,
                BatchPhotoUrl = batch.BatchPhotoUrl,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate,
                CourseId = batch.Course?.CourseId ?? 0, // safe navigation
                CourseName = batch.Course?.CourseName // safe navigation
            };

            return batchDto;
        }


        public static Batch ToEntity(BatchDto dto)
        {
            if (dto == null) return null;

            return new Batch
            {
                // BatchId is auto-generated
                BatchName = dto.BatchName,
                BatchPhotoUrl = dto.BatchPhotoUrl,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CourseId = dto.CourseId // direct FK mapping
            };
        }

    }
}
