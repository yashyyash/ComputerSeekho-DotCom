namespace dotnet_backend.DTOs
{
    public class PlacementDTO
    {
        public int PlacementId { get; set; }
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int PlacedStudents { get; set; }
    }

    public class PlacementCreateDTO
    {
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int PlacedStudents { get; set; }
    }

    public class PlacementUpdateDTO
    {
        public int BatchId { get; set; }
        public int CourseId { get; set; }
        public int RecruiterId { get; set; }
        public int PlacedStudents { get; set; }
    }
}
