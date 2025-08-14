using DotnetBackend.Model;

namespace DotnetBackend.IRepository
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff?> GetByUsernameAsync(string username);
        Task<Staff> AddAsync(Staff staff);
        Task<Staff> UpdateAsync(Staff staff);
        Task<bool> DeleteAsync(int id);
    }
}
