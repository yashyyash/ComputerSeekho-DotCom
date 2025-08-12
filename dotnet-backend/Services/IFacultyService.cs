using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface IFacultyService
    {
        Task<List<FacultyDto>> GetAllFacultiesAsync();
        Task<FacultyDto> GetFacultyByIdAsync(int id);
        Task<FacultyDto> CreateFacultyAsync(FacultyDto dto);
        Task<FacultyDto> UpdateFacultyAsync(int id, FacultyDto dto);
        Task<bool> DeleteFacultyAsync(int id);
    }
}
