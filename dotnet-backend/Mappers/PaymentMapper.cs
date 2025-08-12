using dotnet_backend.DTOs;
using dotnet_backend.Models;
using System.Linq;

namespace dotnet_backend.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentDto ToPaymentDto(Payment payment)
        {
            if (payment == null)
                return null;

            var dto = new PaymentDto
            {
                PaymentId = payment.PaymentId,
                PaymentType = payment.PaymentType,
                StudentId = payment.StudentId,
                TotalAmount = payment.TotalAmount,
                CreatedAt = payment.CreatedAt
            };

            if (payment.Student != null)
            {
                double totalDueAmount = payment.Student.DueAmount;
                dto.DueAmountLeft = totalDueAmount;
                dto.StudentName = payment.Student.StudentName;
            }
            else
            {
                // Optional: set default values if Student is missing
                dto.DueAmountLeft = 0;
                dto.StudentName = string.Empty;
            }

            return dto;
        }


        public static Payment ToPaymentEntity(PaymentDto dto)
        {
            if (dto == null)
                return null;

            var payment = new Payment();

            //payment.PaymentId = dto.PaymentId;
            payment.PaymentType = dto.PaymentType;
            payment.StudentId = dto.StudentId;
            payment.TotalAmount = dto.TotalAmount;
            payment.CreatedAt = dto.CreatedAt;

            

            return payment;
        }
    }
}
