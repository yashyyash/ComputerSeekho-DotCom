using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{
  

    [Table("payment")]
    public class Payment
    {

        [Key]
        [Column("payment_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Column("payment_type")]
        public PaymentType PaymentType { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Column("total_amount")]
        public double TotalAmount { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        

    }


}
