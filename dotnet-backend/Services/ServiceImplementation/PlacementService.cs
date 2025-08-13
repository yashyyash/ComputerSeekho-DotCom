using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Repositories;
using dotnet_backend.Models;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class PlacementService : IPlacementService
    {
        private readonly IPlacementRepository _repo;

        public PlacementService(IPlacementRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PlacementDTO>> GetAllAsync()
        {
            var placements = await _repo.GetAllAsync();
            return placements.Select(p => p.ToPlacementDTO()).ToList();
        }

        public async Task<PlacementDTO> GetByIdAsync(int id)
        {
            var placement = await _repo.GetByIdAsync(id);
            return placement?.ToPlacementDTO();
        }

        public async Task<PlacementDTO> CreateAsync(CreatePlacementDTO dto)
        {
            var placement = dto.ToPlacement();
            var created = await _repo.AddAsync(placement);
            return created.ToPlacementDTO();
        }

        public async Task<bool> UpdateAsync(int id, CreatePlacementDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.BatchId = dto.BatchId;
            existing.CourseId = dto.CourseId;
            existing.RecruiterId = dto.RecruiterId;
            existing.PlacedStudents = dto.PlacedStudents;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
