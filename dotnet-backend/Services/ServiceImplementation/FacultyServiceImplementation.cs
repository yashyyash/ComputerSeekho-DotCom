using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public class FacultyServiceImplementation : IFacultyService
    {
        private readonly IFacultyRepository _repository;

        public FacultyServiceImplementation(IFacultyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FacultyDto>> GetAllFacultiesAsync()
        {
            var faculties = await _repository.GetAllAsync();
            return faculties.Select(FacultyMapper.ToDto).ToList();
        }

        public async Task<FacultyDto> GetFacultyByIdAsync(int id)
        {
            var faculty = await _repository.GetByIdAsync(id);
            return FacultyMapper.ToDto(faculty);
        }

        public async Task<FacultyDto> CreateFacultyAsync(FacultyDto dto)
        {
            var faculty = FacultyMapper.ToEntity(dto);
            var created = await _repository.AddAsync(faculty);
            return FacultyMapper.ToDto(created);
        }

        public async Task<FacultyDto> UpdateFacultyAsync(int id, FacultyDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null;

            // Update fields
            existing.PhotoUrl = dto.PhotoUrl;
            existing.FacultyName = dto.FacultyName;
            existing.TeachingSubject = dto.TeachingSubject;

            var updated = await _repository.UpdateAsync(existing);
            return FacultyMapper.ToDto(updated);
        }

        public async Task<bool> DeleteFacultyAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
