using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IGetInTouchService
    {
        Task<IEnumerable<GetInTouchDTO>> GetAllAsync();
        Task<GetInTouchDTO> GetByIdAsync(int id);
        Task<GetInTouchDTO> AddAsync(GetInTouchDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
