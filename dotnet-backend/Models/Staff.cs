using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{

  

    [Table("staff")]
    public class Staff
    {
        [Key]
        [Column("staff_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }

        [Column("staff_name")]
        public string StaffName { get; set; }

        [Column("staff_username")]
        public string StaffUsername { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("staff_email")]
        public string StaffEmail { get; set; }

        [Column("staff_photo_url")]
        public string StaffPhotoUrl { get; set; }

        [Column("staff_role_id")]
        public int StaffRoleId { get; set; }
        public StaffRole StaffRole { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Enquiry> Enquiries { get; set; }
    }




}
