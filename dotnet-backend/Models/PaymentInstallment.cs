using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models
{



    [Table("payment_installment")]
    public class PaymentInstallment
    {
        [Key]
        [Column("installment_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstallmentId { get; set; }

        [Column("payment_id")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("paid_at")]
        public DateTime PaidAt { get; set; }
    }


}
