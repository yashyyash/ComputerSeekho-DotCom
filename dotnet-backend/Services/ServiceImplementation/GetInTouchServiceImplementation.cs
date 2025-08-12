using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public class GetInTouchServiceImplementation : IGetInTouchService
    {
        private readonly IGetInTouchRepository _repository;

        public GetInTouchServiceImplementation(IGetInTouchRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetInTouchDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(GetInTouchMapper.ToDto).ToList();
        }

        public async Task<GetInTouchDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return GetInTouchMapper.ToDto(entity);
        }

        public async Task<GetInTouchDto> CreateAsync(GetInTouchDto dto)
        {
            var entity = GetInTouchMapper.ToEntity(dto);
            var created = await _repository.AddAsync(entity);
            return GetInTouchMapper.ToDto(created);
        }

        public async Task<GetInTouchDto> UpdateAsync(int id, GetInTouchDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Message = dto.Message;

            var updated = await _repository.UpdateAsync(existing);
            return GetInTouchMapper.ToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
