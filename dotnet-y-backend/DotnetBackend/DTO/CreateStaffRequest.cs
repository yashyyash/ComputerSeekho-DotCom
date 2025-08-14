namespace DotnetBackend.DTO
{
    public class CreateStaffRequest
    {
        public StaffDTO Staff { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
