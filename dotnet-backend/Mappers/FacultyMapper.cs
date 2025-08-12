using dotnet_backend.DTOs;
using dotnet_backend.Models;

namespace dotnet_backend.Mappers
{
    public static class FacultyMapper
    {
        public static FacultyDTO ToDTO(Faculty faculty)
        {
            return new FacultyDTO
            {
                FacultyId = faculty.FacultyId,
                PhotoUrl = faculty.PhotoUrl,
                FacultyName = faculty.FacultyName,
                TeachingSubject = faculty.TeachingSubject
            };
        }

        public static Faculty ToEntity(FacultyDTO dto)
        {
            return new Faculty
            {
                FacultyId = dto.FacultyId,
                PhotoUrl = dto.PhotoUrl,
                FacultyName = dto.FacultyName,
                TeachingSubject = dto.TeachingSubject
            };
        }
    }
}
