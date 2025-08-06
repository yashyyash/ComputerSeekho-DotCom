//package com.seekho.api.entity;
//
//import jakarta.persistence.*;
//
//@Entity
//@Table(name = "payment_type_master")
//public class PaymentTypeMaster {
//
//    @Id
//    @GeneratedValue(strategy = GenerationType.IDENTITY)
//    @Column(name = "payment_type_id")
//    private int paymentTypeId;
//
//    @Column(name = "payment_type_desc")
//    private String paymentTypeDesc;
//
//    // Getters and Setters
//
//    public int getPaymentTypeId() {
//        return paymentTypeId;
//    }
//
//    public void setPaymentTypeId(int paymentTypeId) {
//        this.paymentTypeId = paymentTypeId;
//    }
//
//    public String getPaymentTypeDesc() {
//        return paymentTypeDesc;
//    }
//
//    public void setPaymentTypeDesc(String paymentTypeDesc) {
//        this.paymentTypeDesc = paymentTypeDesc;
//    }
//}

package com.seekho.api.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "payment_type_master")
public class PaymentTypeMaster {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int payment_type_id;

    private String payment_type_desc;

    // Getters and Setters
    public int getPayment_type_id() {
        return payment_type_id;
    }

    public void setPayment_type_id(int payment_type_id) {
        this.payment_type_id = payment_type_id;
    }

    public String getPayment_type_desc() {
        return payment_type_desc;
    }

    public void setPayment_type_desc(String payment_type_desc) {
        this.payment_type_desc = payment_type_desc;
    }


}

