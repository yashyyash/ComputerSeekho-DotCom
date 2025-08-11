using dotnet_backend.Models;

namespace dotnet_backend.Services
{
    public interface IStaffService
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff> GetByIdAsync(int id);
        Task<Staff> CreateAsync(Staff staff, string plainPassword);
        Task<Staff> UpdateAsync(int id, Staff staff);
        Task<bool> DeleteAsync(int id);
    }
}
