using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
    

    [Table("placement")]
    public class Placement
    {
        [Key]
        [Column("placement_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlacementId { get; set; }

        [Column("batch_id")]
        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Column("recruiter_id")]
        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        //[Column("placed_students_id")]
        //public int PlacedStudents { get; set; }
    }

}
