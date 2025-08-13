using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto);
        Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto);
        Task<bool> DeletePaymentAsync(int id);


        Task<byte[]> GeneratePaymentPdfAsync(int paymentId);

        Task<IEnumerable<PaymentDto>> GetPaymentsByStudentIdAsync(int studentId);
    }
}
