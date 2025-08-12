namespace dotnet_backend.DTOs
{
    public class FacultyDto
    {
        public int FacultyId { get; set; }          // For responses (read-only)
        public string PhotoUrl { get; set; }
        public string FacultyName { get; set; }
        public string TeachingSubject { get; set; }
    }
}
