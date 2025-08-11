using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
    
    [Table("staff_role")]
    public class StaffRole
    {
        [Key]
        [Column("staff_role_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffRoleId { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        public ICollection<Staff> StaffMembers { get; set; }
    }

}
