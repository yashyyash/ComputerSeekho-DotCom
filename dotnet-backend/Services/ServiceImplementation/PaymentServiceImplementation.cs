using dotnet_backend.AppDbContext;
using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class PaymentServiceImplementation : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentServiceImplementation(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments
                .Include(p => p.Student)
                
                .ToListAsync();

            return payments.Select(p => PaymentMapper.ToPaymentDto(p));
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Student)
                
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            return PaymentMapper.ToPaymentDto(payment);
        }

        //public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        //{
        //    var payment = PaymentMapper.ToPaymentEntity(paymentDto);
        //    _context.Payments.Add(payment);
        //    await _context.SaveChangesAsync();

        //    // Reload with Student navigation property
        //    var savedPayment = await _context.Payments
        //        .Include(p => p.Student)
        //        .FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);

        //    return PaymentMapper.ToPaymentDto(savedPayment);
        //}


        public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var payment = PaymentMapper.ToPaymentEntity(paymentDto);

            // Add payment
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Find the student and update due amount
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == payment.StudentId);

            if (student != null)
            {
                student.DueAmount -= payment.TotalAmount;
                if (student.DueAmount < 0)
                {
                    student.DueAmount = 0; // Prevent negative due
                }
                await _context.SaveChangesAsync();
            }

            // Reload with Student navigation property for return
            var savedPayment = await _context.Payments
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);

            return PaymentMapper.ToPaymentDto(savedPayment);
        }


        public async Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto)
        {
            var existingPayment = await _context.Payments.FindAsync(id);
            if (existingPayment == null)
                return null;

            // Update properties
            existingPayment.PaymentType = paymentDto.PaymentType;
            existingPayment.StudentId = paymentDto.StudentId;
            existingPayment.TotalAmount = paymentDto.TotalAmount;
            existingPayment.CreatedAt = paymentDto.CreatedAt;

            await _context.SaveChangesAsync();

            return PaymentMapper.ToPaymentDto(existingPayment);
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();


            return true;
        }

        public async Task<byte[]> GeneratePaymentPdfAsync(int paymentId)
        {
            // TODO: Fetch payment info from database here based on paymentId
            // For demo, let's use some dummy data:
            var paymentInfo = new
            {
                PaymentId = paymentId,
                Amount = 1500.75m,
                PaidAt = DateTime.Now,
                StudentName = "John Doe",
                PaymentType = "Credit Card"
            };

            // Create a new PDF document
            using var document = new PdfDocument();
            var page = document.AddPage();

            using var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 20, XFontStyle.Bold);
            var fontSmall = new XFont("Verdana", 12, XFontStyle.Regular);

            // Title
            gfx.DrawString("Payment Receipt", font, XBrushes.Black, new XRect(0, 20, page.Width, 40), XStringFormats.TopCenter);

            // Details
            gfx.DrawString($"Payment ID: {paymentInfo.PaymentId}", fontSmall, XBrushes.Black, 40, 80);
            gfx.DrawString($"Student Name: {paymentInfo.StudentName}", fontSmall, XBrushes.Black, 40, 110);
            gfx.DrawString($"Amount Paid: ₹{paymentInfo.Amount}", fontSmall, XBrushes.Black, 40, 140);
            gfx.DrawString($"Payment Type: {paymentInfo.PaymentType}", fontSmall, XBrushes.Black, 40, 170);
            gfx.DrawString($"Paid At: {paymentInfo.PaidAt:dd-MM-yyyy HH:mm}", fontSmall, XBrushes.Black, 40, 200);

            // Save PDF to memory stream
            using var stream = new MemoryStream();
            document.Save(stream, false);

            // Return PDF bytes
            return stream.ToArray();
        }
    }
}
