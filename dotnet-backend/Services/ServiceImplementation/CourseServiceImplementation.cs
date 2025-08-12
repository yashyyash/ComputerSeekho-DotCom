using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;

namespace dotnet_backend.Services
{
    public class CourseServiceImplementation : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseServiceImplementation(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(CourseMapper.ToDto).ToList();
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return CourseMapper.ToDto(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var course = CourseMapper.ToEntity(courseDto);
            await _courseRepository.AddAsync(course);
            return CourseMapper.ToDto(course);
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            if (!await _courseRepository.ExistsAsync(id))
                return false;

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return false;

            course.CourseName = courseDto.CourseName;
            course.CourseFee = courseDto.CourseFee;
            course.CoursePhotoUrl = courseDto.CoursePhotoUrl;
            course.DurationMonths = courseDto.DurationMonths;
            course.Syllabus = courseDto.Syllabus;

            await _courseRepository.UpdateAsync(course);
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            if (!await _courseRepository.ExistsAsync(id))
                return false;

            await _courseRepository.DeleteAsync(id);
            return true;
        }
    }
}
