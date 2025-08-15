using dotnet_backend.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Reflection.Metadata;

namespace dotnet_backend.Helpers
{
    public static class ReceiptPdfGenerator
    {
        public static byte[] Generate(Payment payment)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            var doc = new iText.Layout.Document(pdf);

            doc.Add(new Paragraph($"Receipt for Payment #{payment.PaymentId}"));
            doc.Add(new Paragraph($"Date: {payment.PaymentDate?.ToString("yyyy-MM-dd")}"));
            doc.Add(new Paragraph($"Amount: ₹{payment.Amount}"));
            doc.Add(new Paragraph($"Payment Type: {payment.PaymentType}"));
            doc.Add(new Paragraph($"Student ID: {payment.StudentId}"));

            doc.Close();
            return ms.ToArray();
        }
    }

}