using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;

namespace dotnet_backend.Services
{
    public class PlacementServiceImplementation : IPlacementService
    {
        private readonly IPlacementRepository _placementRepository;

        public PlacementServiceImplementation(IPlacementRepository placementRepository)
        {
            _placementRepository = placementRepository;
        }

        public async Task<IEnumerable<PlacementDTO>> GetAllAsync()
        {
            var placements = await _placementRepository.GetAllAsync();
            var dtoList = new List<PlacementDTO>();
            foreach (var placement in placements)
            {
                dtoList.Add(PlacementMapper.ToDto(placement));
            }
            return dtoList;
        }

        public async Task<PlacementDTO> GetByIdAsync(int id)
        {
            var placement = await _placementRepository.GetByIdAsync(id);
            return placement == null ? null : PlacementMapper.ToDto(placement);
        }

        public async Task<PlacementDTO> CreateAsync(PlacementCreateDTO dto)
        {
            var placement = PlacementMapper.ToEntity(dto);
            await _placementRepository.AddAsync(placement);
            return PlacementMapper.ToDto(placement);
        }

        public async Task<PlacementDTO> UpdateAsync(int id, PlacementUpdateDTO dto)
        {
            var existingPlacement = await _placementRepository.GetByIdAsync(id);
            if (existingPlacement == null) return null;

            PlacementMapper.UpdateEntity(existingPlacement, dto);
            await _placementRepository.UpdateAsync(existingPlacement);
            return PlacementMapper.ToDto(existingPlacement);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingPlacement = await _placementRepository.GetByIdAsync(id);
            if (existingPlacement == null) return false;

            return await _placementRepository.DeleteAsync(existingPlacement);
        }
    }
}
