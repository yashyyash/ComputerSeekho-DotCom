using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public class AnnouncementServiceImplementation : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementServiceImplementation(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AnnouncementDto>> GetAllAnnouncementsAsync()
        {
            var announcements = await _repository.GetAllAsync();
            return announcements.Select(AnnouncementMapper.ToDto).ToList();
        }

        public async Task<AnnouncementDto> GetAnnouncementByIdAsync(int id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            return AnnouncementMapper.ToDto(announcement);
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto dto)
        {
            var announcement = AnnouncementMapper.ToEntity(dto);
            var created = await _repository.AddAsync(announcement);
            return AnnouncementMapper.ToDto(created);
        }

        public async Task<AnnouncementDto> UpdateAnnouncementAsync(int id, AnnouncementDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null;

            // Update fields (only AnnouncementText)
            existing.AnnouncementText = dto.AnnouncementText;

            var updated = await _repository.UpdateAsync(existing);
            return AnnouncementMapper.ToDto(updated);
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
