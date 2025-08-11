using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
   

    [Table("get_in_touch")]
    public class GetInTouch
    {
        [Key]
        [Column("contact_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("message")]
        public string Message { get; set; }
    }


}
