using dotnet_backend.DTOs;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using dotnet_backend.Mappers;

namespace dotnet_backend.Services
{
    public class StudentServiceImplementation : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IEnquiryRepository _enquiryRepository;

        public StudentServiceImplementation(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IBatchRepository batchRepository,
            IEnquiryRepository enquiryRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _batchRepository = batchRepository;
            _enquiryRepository = enquiryRepository;
        }


        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return StudentMapper.ToDtoList(students);
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return StudentMapper.ToDto(student);
        }

        public async Task<StudentDTO> CreateAsync(StudentCreateDTO studentDto)
        {
            // Validate course
            var course = await _courseRepository.GetByIdAsync(studentDto.CourseId);
            if (course == null)
                throw new Exception($"Course with ID {studentDto.CourseId} not found");

            // Validate batch
            var batch = await _batchRepository.GetByIdAsync(studentDto.BatchId);
            if (batch == null)
                throw new Exception($"Batch with ID {studentDto.BatchId} not found");

            // Map student entity
            var student = StudentMapper.ToEntity(studentDto);

            var enquiry = await _enquiryRepository.GetByIdAsync(studentDto.EnquiryId);
            if (enquiry == null)
                throw new Exception($"Enquiry with ID {studentDto.EnquiryId} not found");


            // Set due amount from course
            student.DueAmount = course.CourseFee;

            // Save
            await _studentRepository.AddAsync(student);
            return StudentMapper.ToDto(student);
        }

        public async Task<StudentDTO?> UpdateAsync(int id, StudentUpdateDTO studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null) return null;

            StudentMapper.UpdateEntity(existingStudent, studentDto);
            await _studentRepository.UpdateAsync(existingStudent);
            return StudentMapper.ToDto(existingStudent);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return false;

            await _studentRepository.DeleteAsync(student);
            return true;
        }
    }
}
