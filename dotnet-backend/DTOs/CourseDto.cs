namespace dotnet_backend.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public double CourseFee { get; set; }
        public string CoursePhotoUrl { get; set; }
        public int DurationMonths { get; set; }
        public string Syllabus { get; set; }
    }
}
