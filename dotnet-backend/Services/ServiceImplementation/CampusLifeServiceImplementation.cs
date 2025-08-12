using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public class CampusLifeServiceImplementation : ICampusLifeService
    {
        private readonly ICampusLifeRepository _repository;

        public CampusLifeServiceImplementation(ICampusLifeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CampusLifeDto>> GetAllCampusLifeAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(CampusLifeMapper.ToDto).ToList();
        }

        public async Task<CampusLifeDto> GetCampusLifeByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return CampusLifeMapper.ToDto(entity);
        }

        public async Task<CampusLifeDto> CreateCampusLifeAsync(CampusLifeDto dto)
        {
            var entity = CampusLifeMapper.ToEntity(dto);
            var created = await _repository.AddAsync(entity);
            return CampusLifeMapper.ToDto(created);
        }

        public async Task<CampusLifeDto> UpdateCampusLifeAsync(int id, CampusLifeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null;

            existing.PhotoUrl = dto.PhotoUrl;
            existing.Description = dto.Description;

            var updated = await _repository.UpdateAsync(existing);
            return CampusLifeMapper.ToDto(updated);
        }

        public async Task<bool> DeleteCampusLifeAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
