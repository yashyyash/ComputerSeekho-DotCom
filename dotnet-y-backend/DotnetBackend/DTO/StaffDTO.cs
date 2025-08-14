// File: DTO/StaffDTO.cs
namespace DotnetBackend.DTO
{
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PrimaryNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // ADMIN, HR, MARKETING
    }
}
