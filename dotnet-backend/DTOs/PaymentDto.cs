using dotnet_backend.Models;

namespace dotnet_backend.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public PaymentType PaymentType { get; set; } // using your enum
        public int StudentId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public double DueAmountLeft { get; set; } // This will be calculated based on the installments

        public string StudentName { get; set; } // Assuming you want to include the student's name

        

    }
}
