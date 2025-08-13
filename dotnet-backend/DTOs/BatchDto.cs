namespace dotnet_backend.DTOs
{
    public class BatchDto
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string BatchPhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; } // Assuming you want to include the course ID



        public string? CourseName { get; set; } // Assuming you want to include the course name
        
    }
}
