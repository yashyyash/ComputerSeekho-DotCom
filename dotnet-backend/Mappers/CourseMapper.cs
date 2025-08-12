using dotnet_backend.Models;
using dotnet_backend.DTOs;

namespace dotnet_backend.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToDto(Course course)
        {
            if (course == null) return null;

            return new CourseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseFee = course.CourseFee,
                CoursePhotoUrl = course.CoursePhotoUrl,
                DurationMonths = course.DurationMonths,
                Syllabus = course.Syllabus
            };
        }

        public static Course ToEntity(CourseDto dto)
        {
            if (dto == null) return null;

            return new Course
            {
                //CourseId = dto.CourseId,   auto incremented
                CourseName = dto.CourseName,
                CourseFee = dto.CourseFee,
                CoursePhotoUrl = dto.CoursePhotoUrl,
                DurationMonths = dto.DurationMonths,
                Syllabus = dto.Syllabus
            };
        }
    }
}
