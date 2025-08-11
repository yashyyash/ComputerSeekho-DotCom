using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_backend.Models
{
   

    [Table("batch_course")]
    public class BatchCourse
    {
        [Column("batch_id")]
        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }


}
