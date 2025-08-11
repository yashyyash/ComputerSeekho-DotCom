using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{

    

    [Table("student")]
    public class Student
    {
        [Key]
        [Column("student_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Column("enquiry_id")]
        public int EnquiryId { get; set; }
        public Enquiry Enquiry { get; set; }

        [Column("student_name")]
        public string StudentName { get; set; }

        [Column("student_photo_url")]
        public string StudentPhotoUrl { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("dob")]
        public DateTime Dob { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Column("batch_id")]
        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        [Column("recruiter_id")]
        public int? RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        [Column("due_amount")]
        public decimal DueAmount { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }


}
