using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetByIdAsync(int id);
        Task<StudentDTO> CreateAsync(StudentCreateDTO studentDto);
        Task<StudentDTO> UpdateAsync(int id, StudentUpdateDTO studentDto);
        Task<bool> DeleteAsync(int id);
    }
}
