using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{



    [Table("course")]
    public class Course
    {
        [Key]
        [Column("course_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("course_fee")]
        public double CourseFee { get; set; }

        [Column("course_photo_url")]
        public string CoursePhotoUrl { get; set; }

        [Column("duration_months")]
        public int DurationMonths { get; set; }

        [Column("syllabus")]
        public string Syllabus { get; set; }

        public ICollection<Batch> Batches { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }
        public ICollection<Placement> Placements { get; set; }
    }


}
