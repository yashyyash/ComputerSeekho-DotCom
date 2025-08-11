using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
 

    [Table("enquiry")]
    public class Enquiry
    {
        [Key]
        [Column("enquiry_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryId { get; set; }

        [Column("staff_id")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Column("enquirer_name")]
        public string EnquirerName { get; set; }

        [Column("enquirer_relation")]
        public EnquirerRelation EnquirerRelation { get; set; }

        [Column("enquiry_address")]
        public string EnquiryAddress { get; set; }

        [Column("inquirer_email")]
        public string InquirerEmail { get; set; }

        [Column("student_name")]
        public string StudentName { get; set; }

        [Column("student_age")]
        public int StudentAge { get; set; }

        [Column("student_gender")]
        public string StudentGender { get; set; }

        [Column("student_dob")]
        public DateTime StudentDob { get; set; }

        [Column("student_email")]
        public string StudentEmail { get; set; }

        [Column("student_photo_url")]
        public string StudentPhotoUrl { get; set; }

        [Column("enquiry_query")]
        public string EnquiryQuery { get; set; }

        [Column("status")]
        public EnquiryStatus Status { get; set; }

        [Column("closure_reason_id")]
        public int? ClosureReasonId { get; set; }
        public ClosureReason ClosureReason { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public ICollection<FollowUp> FollowUps { get; set; }
        public ICollection<Student> Students { get; set; }

    }


}
