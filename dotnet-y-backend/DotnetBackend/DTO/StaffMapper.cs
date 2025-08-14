using DotnetBackend.Model;

namespace DotnetBackend.DTO
{
    public static class StaffMapper
    {
        public static StaffDTO ToDTO(this Staff staff)
        {
            return new StaffDTO
            {
                StaffId = staff.StaffId,
                Name = staff.Name,
                Email = staff.Email,
                Username = staff.Username,
                PrimaryNumber = staff.PrimaryNumber,
                Role = staff.Role
            };
        }
    }
}
