import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './PaymentPage.css';

const PAYMENT_TYPES = {
  1: "Cash",
  2: "Credit Card",
  3: "Debit Card",
  4: "UPI",
  5: "Bank Transfer",
  6: "Cheque",
};

const PaymentPage = () => {
  const { studentId } = useParams();
  const [payments, setPayments] = useState([]);
  const [dueAmount, setDueAmount] = useState(null); // New state for due amount
  const [formData, setFormData] = useState({
    totalAmount: '',
    createdAt: new Date().toISOString().split('T')[0],
    paymentType: '',
    studentName: ''
  });

  const fetchPayments = async () => {
    try {
      const response = await fetch(`http://localhost:8080/api/payment/student/${studentId}`);
      const data = await response.json();
      console.log("Fetched payments:", data);
      setPayments(Array.isArray(data) ? data : []);

      // Auto-fill studentName if available
      if (Array.isArray(data) && data.length > 0) {
        setFormData(prev => ({ ...prev, studentName: data[0].studentName || '' }));
      }
    } catch (error) {
      console.error('Error fetching payments:', error);
    }
  };

  const fetchStudentDetails = async () => {
    try {
      const res = await fetch(`http://localhost:8080/api/student/${studentId}`);
      const student = await res.json();
      console.log("Fetched student:", student);
      setDueAmount(student.dueAmount ?? null);

      // Auto-fill student name if not set from payments
      if (!formData.studentName) {
        setFormData(prev => ({ ...prev, studentName: student.studentName || '' }));
      }
    } catch (error) {
      console.error("Error fetching student details:", error);
    }
  };

  useEffect(() => {
    if (studentId) {
      fetchPayments();
      fetchStudentDetails();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [studentId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const payload = {
      paymentType: parseInt(formData.paymentType),
      studentId: parseInt(studentId),
      totalAmount: parseFloat(formData.totalAmount),
      createdAt: formData.createdAt + "T00:00:00",
      studentName: formData.studentName
    };

    console.log("POST payload:", payload);

    try {
      const response = await fetch('http://localhost:8080/api/payment', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });

      if (response.ok) {
        setFormData({
          totalAmount: '',
          createdAt: new Date().toISOString().split('T')[0],
          paymentType: '',
          studentName: formData.studentName
        });
        fetchPayments();
        fetchStudentDetails(); // Refresh due amount after payment
      } else {
        const errorText = await response.text();
        console.error("Server error:", errorText);
        alert('Failed to create payment.');
      }
    } catch (error) {
      console.error('Error posting payment:', error);
    }
  };

  return (
    <div className="payment-container">
      <h2>Create Payment for Student ID: {studentId}</h2>
      {dueAmount !== null && (
        <p style={{ textAlign: "center", fontWeight: "bold", color: dueAmount > 0 ? "red" : "green" }}>
          Due Amount: ₹{dueAmount.toFixed(2)}
        </p>
      )}
      
      <form onSubmit={handleSubmit} className="payment-form">
        <input
          type="text"
          name="studentName"
          placeholder="Student Name"
          value={formData.studentName}
          onChange={handleChange}
          required
        />
        <input
          type="number"
          name="totalAmount"
          placeholder="Amount"
          value={formData.totalAmount}
          onChange={handleChange}
          required
        />
        <input
          type="date"
          name="createdAt"
          value={formData.createdAt}
          onChange={handleChange}
          required
        />
        <select
          name="paymentType"
          value={formData.paymentType}
          onChange={handleChange}
          required
        >
          <option value="">Select Payment Type</option>
          {Object.entries(PAYMENT_TYPES).map(([key, label]) => (
            <option key={key} value={key}>{label}</option>
          ))}
        </select>
        <button type="submit">Submit Payment</button>
      </form>

      <h3>Payments History</h3>
      <table className="payment-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Student Name</th>
            <th>Amount</th>
            <th>Date</th>
            <th>Payment Type</th>
            <th>Receipt</th>
          </tr>
        </thead>
        <tbody>
          {payments.length > 0 ? (
            payments.map(payment => (
              <tr key={payment.paymentId}>
                <td>{payment.paymentId}</td>
                <td>{payment.studentName}</td>
                <td>₹{payment.totalAmount.toFixed(2)}</td>
                <td>{payment.createdAt.split('T')[0]}</td>
                <td>{PAYMENT_TYPES[payment.paymentType] || 'Unknown'}</td>
                <td>
                  <a
                    href={`http://localhost:8080/api/payment/${payment.paymentId}/pdf`}
                    target="_blank"
                    rel="noopener noreferrer"
                  >
                    <button type="button" className="receipt-btn">Receipt</button>
                  </a>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6" style={{ textAlign: 'center' }}>No payments found</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default PaymentPage;
