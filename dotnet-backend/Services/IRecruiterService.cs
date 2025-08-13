using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IRecruiterService
    {
        Task<IEnumerable<RecruiterDTO>> GetAllAsync();
        Task<RecruiterDTO> GetByIdAsync(int id);
        Task<RecruiterDTO> CreateAsync(RecruiterCreateDTO dto);
        Task<RecruiterDTO> UpdateAsync(int id, RecruiterUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
