using dotnet_backend.Models;

namespace dotnet_backend.DTOs
{
    public class EnquiryRequestDto
    {
        public int StaffId { get; set; }
        public int CourseId { get; set; }
        public string EnquirerName { get; set; }
        public string EnquiryAddress { get; set; }
        public string InquirerEmail { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public string StudentGender { get; set; }
        public DateTime StudentDob { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhotoUrl { get; set; }
        public string EnquiryQuery { get; set; }
        public EnquiryStatus Status { get; set; }
    }

    public class EnquiryResponseDto
    {
        public int EnquiryId { get; set; }
        public string EnquirerName { get; set; }
        public string StudentName { get; set; }
        public string Status { get; set; }
        public string ClosureReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<FollowUpDto> FollowUps { get; set; }
    }

    public class FollowUpDto
    {
        public int FollowupId { get; set; }
        public DateTime FollowupDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
