using DotnetBackend.DTO;

namespace DotnetBackend.IService
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDTO>> GetAllAsync();
        Task<StaffDTO?> GetByIdAsync(int id);
        Task<StaffDTO> AddAsync(StaffDTO dto, string password); // match controller
        Task<StaffDTO?> UpdateAsync(StaffDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto);
    }
}
