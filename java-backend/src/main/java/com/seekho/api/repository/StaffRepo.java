package com.seekho.api.repository;


import com.seekho.api.entity.Staff;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface StaffRepo extends JpaRepository<Staff, Long> {
    Staff findByStaffUsername(String staffUsername);
}
