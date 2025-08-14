// File: Model/Staff.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetBackend.Model
{
    [Table("staff")]
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        // store a hash, never plain password
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PrimaryNumber { get; set; } = string.Empty;

        // ADMIN, HR, MARKETING
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
