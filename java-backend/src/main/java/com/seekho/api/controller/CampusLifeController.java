package com.seekho.api.controller;

import com.seekho.api.dto.CampusLifeDto;
import com.seekho.api.service.CampusLifeService;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins = "http://localhost:5173")
@RestController
@RequestMapping("/api/campus-life")
public class CampusLifeController {

    private static final Logger logger = LogManager.getLogger(CampusLifeController.class);

    @Autowired
    private CampusLifeService campusLifeService;

    // Create
    @PostMapping
    public ResponseEntity<CampusLifeDto> createCampusLife(@RequestBody CampusLifeDto campusLifeDto) {
        logger.info("Creating new CampusLife entry: {}", campusLifeDto);
        CampusLifeDto savedDto = campusLifeService.createCampusLife(campusLifeDto);
        logger.debug("Saved CampusLife: {}", savedDto);
        return ResponseEntity.ok(savedDto);
    }

    // Read all
    @GetMapping
    public ResponseEntity<List<CampusLifeDto>> getAllCampusLife() {
        logger.info("Fetching all CampusLife entries");
        List<CampusLifeDto> campusLifeList = campusLifeService.getAllCampusLife();
        logger.debug("Found {} entries", campusLifeList.size());
        return ResponseEntity.ok(campusLifeList);
    }

    // Read by ID
    @GetMapping("/{id}")
    public ResponseEntity<CampusLifeDto> getCampusLifeById(@PathVariable Long id) {
        logger.info("Fetching CampusLife by ID: {}", id);
        CampusLifeDto campusLifeDto = campusLifeService.getCampusLifeById(id);
        return ResponseEntity.ok(campusLifeDto);
    }

    // Update
    @PutMapping("/{id}")
    public ResponseEntity<CampusLifeDto> updateCampusLife(
            @PathVariable Long id,
            @RequestBody CampusLifeDto campusLifeDto
    ) {
        logger.info("Updating CampusLife with ID: {}", id);
        CampusLifeDto updatedDto = campusLifeService.updateCampusLife(id, campusLifeDto);
        logger.debug("Updated CampusLife: {}", updatedDto);
        return ResponseEntity.ok(updatedDto);
    }

    // Delete
    @DeleteMapping("/{id}")
    public ResponseEntity<String> deleteCampusLife(@PathVariable Long id) {
        logger.warn("Deleting CampusLife with ID: {}", id);
        campusLifeService.deleteCampusLife(id);
        return ResponseEntity.ok("CampusLife with ID " + id + " deleted successfully");
    }
}
