using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IPlacementService
    {
        Task<IEnumerable<PlacementDTO>> GetAllAsync();
        Task<PlacementDTO> GetByIdAsync(int id);
        Task<PlacementDTO> CreateAsync(PlacementCreateDTO dto);
        Task<PlacementDTO> UpdateAsync(int id, PlacementUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
