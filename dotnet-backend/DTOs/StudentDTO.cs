namespace dotnet_backend.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public double DueAmount { get; set; }

        public string StudentPhotoUrl { get; set; }
    }

    public class StudentCreateDTO
    {
        public int EnquiryId { get; set; }
        public string StudentName { get; set; }
        public string StudentPhotoUrl { get; set; }
        public int Age { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public int CourseId { get; set; }
        public int BatchId { get; set; }
        public int? RecruiterId { get; set; }
        public double DueAmount { get; set; }
    }

    public class StudentUpdateDTO
    {
        public string StudentName { get; set; }
        public string StudentPhotoUrl { get; set; }
        public int Age { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public double DueAmount { get; set; }
    }
}
