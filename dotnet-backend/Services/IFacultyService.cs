using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDTO>> GetAllAsync();
        Task<FacultyDTO> GetByIdAsync(int id);
        Task<FacultyDTO> CreateAsync(FacultyDTO facultyDto);
        Task<FacultyDTO> UpdateAsync(FacultyDTO facultyDto);
        Task<bool> DeleteAsync(int id);
    }
}
