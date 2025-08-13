using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class PlacementServiceImplementation : IPlacementService
    {
        private readonly IPlacementRepository _placementRepository;

        public PlacementServiceImplementation(IPlacementRepository placementRepository)
        {
            _placementRepository = placementRepository;
        }

        public async Task<IEnumerable<PlacementDto>> GetAllPlacementsAsync()
        {
            var placements = await _placementRepository.GetAllAsync();
            return placements.Select(PlacementMapper.ToDto);
        }

        public async Task<IEnumerable<PlacementDto>> GetPlacementsByBatchIdAsync(int batchId)
        {
            var placements = await _placementRepository.GetByBatchIdAsync(batchId);
            return placements.Select(PlacementMapper.ToDto);
        }

        public async Task<PlacementDto> GetPlacementByIdAsync(int id)
        {
            var placement = await _placementRepository.GetByIdAsync(id);
            return PlacementMapper.ToDto(placement);
        }

        public async Task<PlacementDto> CreatePlacementAsync(PlacementDto placementDto)
        {
            var placement = PlacementMapper.ToEntity(placementDto); // PlacementId is not set
            await _placementRepository.AddAsync(placement);

            // Reload with navigation properties
            var createdPlacement = await _placementRepository.GetByIdAsync(placement.PlacementId);
            return PlacementMapper.ToDto(createdPlacement);
        }


        public async Task<PlacementDto> UpdatePlacementAsync(int id, PlacementDto placementDto)
        {
            var existing = await _placementRepository.GetByIdAsync(id);
            if (existing == null) return null;

            // Update entity fields
            existing.BatchId = placementDto.BatchId;
            existing.CourseId = placementDto.CourseId;
            existing.RecruiterId = placementDto.RecruiterId;
            existing.StudentId = placementDto.StudentId;

            await _placementRepository.UpdateAsync(existing);

            var updated = await _placementRepository.GetByIdAsync(id);
            return PlacementMapper.ToDto(updated);
        }

        public async Task<bool> DeletePlacementAsync(int id)
        {
            var existing = await _placementRepository.GetByIdAsync(id);
            if (existing == null) return false;

            return await _placementRepository.DeleteAsync(existing);
        }
    }
}
