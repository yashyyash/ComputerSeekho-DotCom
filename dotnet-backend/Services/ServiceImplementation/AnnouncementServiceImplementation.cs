using dotnet_backend.DTOs;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using dotnet_backend.Mappers;

namespace dotnet_backend.Services
{
    public class AnnouncementServiceImplementation : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementServiceImplementation(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAllAsync()
        {
            var announcements = await _repository.GetAllAsync();
            return announcements.Select(a => AnnouncementMapper.ToDTO(a));
        }

        public async Task<AnnouncementDTO> GetByIdAsync(int id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            return announcement == null ? null : AnnouncementMapper.ToDTO(announcement);
        }

        public async Task<AnnouncementDTO> AddAsync(AnnouncementDTO dto)
        {
            var entity = AnnouncementMapper.ToEntity(dto);
            var savedEntity = await _repository.AddAsync(entity);
            return AnnouncementMapper.ToDTO(savedEntity);
        }

        public async Task<AnnouncementDTO> UpdateAsync(AnnouncementDTO dto)
        {
            var entity = AnnouncementMapper.ToEntity(dto);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return AnnouncementMapper.ToDTO(updatedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

