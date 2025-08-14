namespace DotnetBackend.DTO
{
    public class LoginResponseDTO
    {
        public int StaffId { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
