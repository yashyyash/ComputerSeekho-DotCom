namespace dotnet_backend.DTOs
{
    public class PlacementDto
    {
        // Core IDs (required)
        public int PlacementId { get; set; }
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int StudentId { get; set; }

        // Optional display fields
        public string? StudentName { get; set; }
        public string? StudentPhotoUrl { get; set; }
        public string? CompanyName { get; set; }
        public string? BatchName { get; set; }
        public string? RecuriterName { get; set; }
    }
}
