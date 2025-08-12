using dotnet_backend.DTOs;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using dotnet_backend.Mappers;

namespace dotnet_backend.Services
{
    public class StudentServiceImplementation : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServiceImplementation(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
            var student = StudentMapper.ToEntity(studentDto);
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
