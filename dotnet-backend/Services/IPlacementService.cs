using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IPlacementService
    {
        Task<IEnumerable<PlacementDTO>> GetAllAsync();
        Task<PlacementDTO> GetByIdAsync(int id);
        Task<PlacementDTO> CreateAsync(CreatePlacementDTO dto);
        Task<bool> UpdateAsync(int id, CreatePlacementDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
