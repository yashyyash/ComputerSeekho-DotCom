using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
    

    [Table("closure_reason")]
    public class ClosureReason
    {
        [Key]
        [Column("closure_reason_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClosureReasonId { get; set; }

        [Column("reason_text")]
        public string ReasonText { get; set; }

        public ICollection<Enquiry> Enquiries { get; set; }
    }


}
