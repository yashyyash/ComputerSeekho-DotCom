using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        Task<bool> UpdateCourseAsync(int id, CourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
