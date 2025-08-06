//package com.seekho.api.entity;
//
//import jakarta.persistence.*;
//import java.time.LocalDate;
//
//@Entity
//@Table(name = "payment")
//public class Payment {
//
//    @Id
//    @GeneratedValue(strategy = GenerationType.IDENTITY)
//    @Column(name = "payment_id")
//    private int paymentId;
//
//    @Column(name = "payment_type_id")
//    private int paymentTypeId;
//
//    @Column(name = "payment_date")
//    private LocalDate paymentDate;
//
//    @Column(name = "student_id")
//    private int studentId;
//
//    @Column(name = "course_id")
//    private int courseId;
//
//    @Column(name = "batch_id")
//    private int batchId;
//
//    @Column(name = "amount")
//    private int amount;
//
//    // Getters and Setters
//
//    public int getPaymentId() {
//        return paymentId;
//    }
//
//    public void setPaymentId(int paymentId) {
//        this.paymentId = paymentId;
//    }
//
//    public int getPaymentTypeId() {
//        return paymentTypeId;
//    }
//
//    public void setPaymentTypeId(int paymentTypeId) {
//        this.paymentTypeId = paymentTypeId;
//    }
//
//    public LocalDate getPaymentDate() {
//        return paymentDate;
//    }
//
//    public void setPaymentDate(LocalDate paymentDate) {
//        this.paymentDate = paymentDate;
//    }
//
//    public int getStudentId() {
//        return studentId;
//    }
//
//    public void setStudentId(int studentId) {
//        this.studentId = studentId;
//    }
//
//    public int getCourseId() {
//        return courseId;
//    }
//
//    public void setCourseId(int courseId) {
//        this.courseId = courseId;
//    }
//
//    public int getBatchId() {
//        return batchId;
//    }
//
//    public void setBatchId(int batchId) {
//        this.batchId = batchId;
//    }
//
//    public int getAmount() {
//        return amount;
//    }
//
//    public void setAmount(int amount) {
//        this.amount = amount;
//    }
//}


package com.seekho.api.entity;

import jakarta.persistence.*;

import java.time.LocalDate;
import java.util.List;

@Entity
@Table(name = "payment")
public class Payment {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int payment_id;

    private LocalDate payment_date;
    private Double amount;

    @ManyToOne
    @JoinColumn(name = "student_id")
    private Student student;

    @ManyToOne
    @JoinColumn(name = "payment_type_id")
    private PaymentTypeMaster payment_type;

    @OneToOne(mappedBy = "payment", cascade = CascadeType.ALL, orphanRemoval = true)
    private Receipt receipt;



    // Getters and Setters

    public int getPayment_id() {
        return payment_id;
    }

    public void setPayment_id(int payment_id) {
        this.payment_id = payment_id;
    }


    public LocalDate getPayment_date() {
        return payment_date;
    }

    public void setPayment_date(LocalDate payment_date) {
        this.payment_date = payment_date;
    }
    

    public Double getAmount() {
        return amount;
    }

    public void setAmount(Double amount) {
        this.amount = amount;
    }

    public PaymentTypeMaster getPayment_type() {
        return payment_type;
    }

    public void setPayment_type(PaymentTypeMaster payment_type) {
        this.payment_type = payment_type;
    }

    public Receipt getReceipt() {
        return receipt;
    }

    public void setReceipt(Receipt receipt) {
        this.receipt = receipt;
    }

    public Student getStudent() {
        return student;
    }

    public void setStudent(Student student) {
        this.student = student;
    }
    
}

