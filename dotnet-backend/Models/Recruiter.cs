using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
   

    [Table("recruiter")]
    public class Recruiter
    {
        [Key]
        [Column("recruiter_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecruiterId { get; set; }

        [Column("company_name")]
        public string CompanyName { get; set; }

        [Column("recruiter_photo_url")]
        public string RecruiterPhotoUrl { get; set; }

        public ICollection<Placement> Placements { get; set; }
        public ICollection<Student> Students { get; set; }
    }


}
