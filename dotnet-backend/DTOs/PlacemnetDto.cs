namespace dotnet_backend.DTOs
{
    public class PlacementDTO
    {
        public int PlacementId { get; set; }
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int PlacedStudents { get; set; }

        // Extra display data (optional)
        public string BatchName { get; set; }
        public string CourseName { get; set; }
        public string RecruiterName { get; set; }
    }

    public class CreatePlacementDTO
    {
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int PlacedStudents { get; set; }
    }
}
