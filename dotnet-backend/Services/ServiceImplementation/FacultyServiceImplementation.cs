using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class FacultyServiceImplementation : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyServiceImplementation(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllAsync()
        {
            var faculties = await _facultyRepository.GetAllAsync();
            return faculties.Select(FacultyMapper.ToDTO);
        }

        public async Task<FacultyDTO> GetByIdAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);
            return faculty == null ? null : FacultyMapper.ToDTO(faculty);
        }

        public async Task<FacultyDTO> CreateAsync(FacultyDTO facultyDto)
        {
            var faculty = FacultyMapper.ToEntity(facultyDto);
            var created = await _facultyRepository.AddAsync(faculty);
            return FacultyMapper.ToDTO(created);
        }

        public async Task<FacultyDTO> UpdateAsync(FacultyDTO facultyDto)
        {
            var faculty = FacultyMapper.ToEntity(facultyDto);
            var updated = await _facultyRepository.UpdateAsync(faculty);
            return FacultyMapper.ToDTO(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _facultyRepository.DeleteAsync(id);
        }
    }
}
