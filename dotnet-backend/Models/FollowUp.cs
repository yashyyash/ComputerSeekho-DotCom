using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
  

    [Table("follow_up")]
    public class FollowUp
    {
        [Key]
        [Column("followup_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowupId { get; set; }

        [Column("enquiry_id")]
        public int EnquiryId { get; set; }
        public Enquiry Enquiry { get; set; }

        [Column("followup_date")]
        public DateTime FollowupDate { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }

}
