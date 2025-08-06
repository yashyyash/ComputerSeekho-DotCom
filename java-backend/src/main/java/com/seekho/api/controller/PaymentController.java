package com.seekho.api.controller;

import com.seekho.api.dto.PaymentDTO;
import com.seekho.api.entity.Payment;
import com.seekho.api.mapper.PaymentMapper;
import com.seekho.api.service.PaymentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/payments")
public class PaymentController {

    @Autowired
    private PaymentService paymentService;

    // POST: Create a new payment
    @PostMapping
    public ResponseEntity<PaymentDTO> savePayment(@RequestBody PaymentDTO paymentDTO) {
        Payment savedPayment = paymentService.savePayment(paymentDTO);
        PaymentDTO responseDTO = PaymentMapper.toPaymentDTO(savedPayment);
        return ResponseEntity.ok(responseDTO);
    }

    // GET: Get all payments
    @GetMapping
    public ResponseEntity<List<PaymentDTO>> getAllPayments() {
        List<PaymentDTO> payments = paymentService.getAllPayments()
                .stream()
                .map(PaymentMapper::toPaymentDTO)
                .collect(Collectors.toList());
        return ResponseEntity.ok(payments);
    }

    // GET: Get a specific payment by ID
    @GetMapping("/{id}")
    public ResponseEntity<PaymentDTO> getPaymentById(@PathVariable int id) {
        Payment payment = paymentService.getPaymentById(id);
        return ResponseEntity.ok(PaymentMapper.toPaymentDTO(payment));
    }
}

