import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './PaymentPage.css';

const PaymentPage = () => {
    const { studentId } = useParams();
    const [payments, setPayments] = useState([]);
    const [formData, setFormData] = useState({
        amount: '',
        payment_date: new Date().toISOString().split('T')[0], // today's date
        payment_type: ''
    });

    const fetchPayments = async () => {
        try {
            const response = await fetch(`http://localhost:8080/api/payments/student/${studentId}`);
            const data = await response.json();
            setPayments(data);
        } catch (error) {
            console.error('Error fetching payments:', error);
        }
    };

    useEffect(() => {
        if (studentId) {
            fetchPayments();
        }
    }, [studentId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevState) => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('http://localhost:8080/api/payments', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    amount: parseFloat(formData.amount),
                    payment_date: formData.payment_date,
                    payment_type: parseInt(formData.payment_type),
                    student_id: parseInt(studentId)
                })
            });

            if (response.ok) {
                setFormData({
                    amount: '',
                    payment_date: new Date().toISOString().split('T')[0],
                    payment_type: ''
                });
                fetchPayments();
            } else {
                console.error('Failed to create payment');
            }
        } catch (error) {
            console.error('Error posting payment:', error);
        }
    };

    return (
        <div className="container">
            <h2>Create Payment for Student ID: {studentId}</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="number"
                    name="amount"
                    placeholder="Amount"
                    value={formData.amount}
                    onChange={handleChange}
                    required
                />
                <input
                    type="date"
                    name="payment_date"
                    value={formData.payment_date}
                    onChange={handleChange}
                    required
                />
                <select
                    name="payment_type"
                    value={formData.payment_type}
                    onChange={handleChange}
                    required
                >
                    <option value="">Select Payment Type</option>
                    <option value="1">Cash</option>
                    <option value="2">Credit Card</option>
                    <option value="3">Debit Card</option>
                    <option value="4">UPI</option>
                    <option value="5">Bank Transfer</option>
                    <option value="6">Cheque</option>
                </select>
                <button type="submit">Submit</button>
            </form>

            <h3>Payments List</h3>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Amount</th>
                        <th>Date</th>
                        <th>Payment Type</th>
                        <th>Receipt ID</th>
                        <th>Student ID</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {payments.map((payment) => (
                        <tr key={payment.payment_id}>
                            <td>{payment.payment_id}</td>
                            <td>{payment.amount}</td>
                            <td>{payment.payment_date}</td>
                            <td>{payment.payment_type}</td>
                            <td>{payment.receipt_id}</td>
                            <td>{payment.student_id}</td>
                            <td>
                                {payment.receipt_id ? (
                                    <a
                                        href={`/receipt/${payment.receipt_id}`}
                                        className="view-receipt-btn"
                                        target="_blank"
                                        rel="noopener noreferrer"
                                    >
                                        View Receipt
                                    </a>
                                ) : (
                                    "N/A"
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default PaymentPage;
