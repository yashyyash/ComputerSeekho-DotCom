using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface IGetInTouchService
    {
        Task<List<GetInTouchDto>> GetAllAsync();
        Task<GetInTouchDto> GetByIdAsync(int id);
        Task<GetInTouchDto> CreateAsync(GetInTouchDto dto);
        Task<GetInTouchDto> UpdateAsync(int id, GetInTouchDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
