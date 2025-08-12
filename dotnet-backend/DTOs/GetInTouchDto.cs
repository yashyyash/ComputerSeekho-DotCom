namespace dotnet_backend.DTOs
{
    public class GetInTouchDto
    {
        public int ContactId { get; set; }  // auto-incremented, read-only
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
