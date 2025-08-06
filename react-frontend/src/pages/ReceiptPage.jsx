import React, { useEffect, useState } from "react";
import "./ReceiptPage.css";

const ReceiptPage = () => {
  const [receipt, setReceipt] = useState(null);
  const receiptId = 2; // static for now

  useEffect(() => {
    fetch(`http://localhost:8080/api/receipt/${receiptId}`)
      .then((res) => res.json())
      .then((data) => setReceipt(data))
      .catch((err) => console.error("Error fetching receipt:", err));
  }, []);

  const handleDownloadPDF = () => {
    fetch(`http://localhost:8080/api/receipt/${receiptId}/pdf`, {
      method: "GET",
    })
      .then((response) => response.blob())
      .then((blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = `receipt_${receiptId}.pdf`;
        a.click();
        window.URL.revokeObjectURL(url);
      })
      .catch((error) => console.error("Error downloading PDF:", error));
  };

  if (!receipt) {
    return <div className="receipt-container">Loading receipt...</div>;
  }

  return (
    <div className="receipt-container">
      <h1>Receipt Details</h1>
      <div className="receipt-info">
        <p><strong>Receipt ID:</strong> {receipt.receipt_id}</p>
        <p><strong>Receipt Date:</strong> {receipt.receipt_date}</p>
        <p><strong>Receipt Amount:</strong> ₹{receipt.receipt_amount}</p>
        <p><strong>Payment ID:</strong> {receipt.payment_id}</p>
        <p><strong>Payment Date:</strong> {receipt.payment_date}</p>
        <p><strong>Payment Amount:</strong> ₹{receipt.amount}</p>
        <p><strong>Payment Type:</strong> {receipt.payment_type}</p>
        <p><strong>Student Roll No:</strong> {receipt.student_id}</p>
      </div>
      <button className="download-button" onClick={handleDownloadPDF}>
        Download PDF
      </button>
    </div>
  );
};

export default ReceiptPage;
