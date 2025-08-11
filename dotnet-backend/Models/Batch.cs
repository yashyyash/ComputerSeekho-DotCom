using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
   

    [Table("batch")]
    public class Batch
    {
        [Key]
        [Column("batch_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }

        [Column("batch_name")]
        public string BatchName { get; set; }

        [Column("batch_photo_url")]
        public string BatchPhotoUrl { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        public ICollection<BatchCourse> BatchCourses { get; set; }
        public ICollection<Placement> Placements { get; set; }
        public ICollection<Student> Students { get; set; }
    }

}
