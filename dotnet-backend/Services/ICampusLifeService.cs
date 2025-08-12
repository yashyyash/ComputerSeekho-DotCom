using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public interface ICampusLifeService
    {
        Task<List<CampusLifeDto>> GetAllCampusLifeAsync();
        Task<CampusLifeDto> GetCampusLifeByIdAsync(int id);
        Task<CampusLifeDto> CreateCampusLifeAsync(CampusLifeDto dto);
        Task<CampusLifeDto> UpdateCampusLifeAsync(int id, CampusLifeDto dto);
        Task<bool> DeleteCampusLifeAsync(int id);
    }
}
