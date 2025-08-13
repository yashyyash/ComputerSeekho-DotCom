using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface IPlacementService
    {
        Task<IEnumerable<PlacementDto>> GetAllPlacementsAsync();
        Task<IEnumerable<PlacementDto>> GetPlacementsByBatchIdAsync(int batchId);
        Task<PlacementDto> GetPlacementByIdAsync(int id);
        Task<PlacementDto> CreatePlacementAsync(PlacementDto placementDto);
        Task<PlacementDto> UpdatePlacementAsync(int id, PlacementDto placementDto);
        Task<bool> DeletePlacementAsync(int id);
    }
}
