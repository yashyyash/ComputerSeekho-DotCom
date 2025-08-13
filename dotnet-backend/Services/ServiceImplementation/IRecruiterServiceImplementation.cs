using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;

namespace dotnet_backend.Services
{
    public class RecruiterServiceImplementation : IRecruiterService
    {
        private readonly IRecruiterRepository _recruiterRepository;

        public RecruiterServiceImplementation(IRecruiterRepository recruiterRepository)
        {
            _recruiterRepository = recruiterRepository;
        }

        public async Task<IEnumerable<RecruiterDTO>> GetAllAsync()
        {
            var recruiters = await _recruiterRepository.GetAllAsync();
            var dtoList = new List<RecruiterDTO>();
            foreach (var recruiter in recruiters)
            {
                dtoList.Add(RecruiterMapper.ToDto(recruiter));
            }
            return dtoList;
        }

        public async Task<RecruiterDTO> GetByIdAsync(int id)
        {
            var recruiter = await _recruiterRepository.GetByIdAsync(id);
            return recruiter == null ? null : RecruiterMapper.ToDto(recruiter);
        }

        public async Task<RecruiterDTO> CreateAsync(RecruiterCreateDTO dto)
        {
            var recruiter = RecruiterMapper.ToEntity(dto);
            await _recruiterRepository.AddAsync(recruiter);
            return RecruiterMapper.ToDto(recruiter);
        }

        public async Task<RecruiterDTO> UpdateAsync(int id, RecruiterUpdateDTO dto)
        {
            var existingRecruiter = await _recruiterRepository.GetByIdAsync(id);
            if (existingRecruiter == null) return null;

            RecruiterMapper.UpdateEntity(existingRecruiter, dto);
            await _recruiterRepository.UpdateAsync(existingRecruiter);
            return RecruiterMapper.ToDto(existingRecruiter);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecruiter = await _recruiterRepository.GetByIdAsync(id);
            if (existingRecruiter == null) return false;

            return await _recruiterRepository.DeleteAsync(existingRecruiter);
        }
    }
}
