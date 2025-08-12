using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.PaymentId }, createdPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDto paymentDto)
        {
            var updatedPayment = await _paymentService.UpdatePaymentAsync(id, paymentDto);
            if (updatedPayment == null)
                return NotFound();

            return Ok(updatedPayment);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var deleted = await _paymentService.DeletePaymentAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }



        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GetPaymentPdf(int id)
        {
            var pdfBytes = await _paymentService.GeneratePaymentPdfAsync(id);

            if (pdfBytes == null || pdfBytes.Length == 0)
                return NotFound();

            // Return PDF file
            return File(pdfBytes, "application/pdf", $"payment_{id}.pdf");
        }
    }
}
