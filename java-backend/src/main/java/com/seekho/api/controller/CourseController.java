package com.seekho.api.controller;

import com.seekho.api.entity.Course;
import com.seekho.api.service.CourseService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;
@CrossOrigin(origins = "http://localhost:5173")
@RestController
@RequestMapping("/api/course")
public class CourseController {

    @Autowired
    private CourseService courseService;

    @GetMapping("/byId/{id}")
    public ResponseEntity<Course> getCourseById(@PathVariable int id) {
        Optional<Course> course = courseService.getCourseById(id);
        return course.map(value -> ResponseEntity.status(HttpStatus.OK).body(value))
                .orElseGet(() -> ResponseEntity.status(HttpStatus.NOT_FOUND).build());
    }

    @GetMapping
    public List<Course> getAllCourses() {
        return courseService.getAllCourses();
    }

    @PostMapping
    public ResponseEntity<String> addCourse(@RequestBody Course course) {
        courseService.addCourse(course);
        return ResponseEntity.status(HttpStatus.CREATED).body("Course Added");
    }

    @PutMapping
    public ResponseEntity<String> updateCourse(@RequestBody Course course) {
        boolean isUpdated = courseService.updateCourse(course);
        if (isUpdated)
            return new ResponseEntity<>("Course Details Updated", HttpStatus.OK);
        else
            return new ResponseEntity<>("Course Not Found", HttpStatus.NOT_FOUND);
    }

    @DeleteMapping("/{courseId}")
    public ResponseEntity<String> deleteCourse(@PathVariable int courseId) {
        courseService.deleteCourse(courseId);
        return ResponseEntity.ok("Course Deleted");
    }

    @GetMapping("/byName/{courseName}")
    public ResponseEntity<Course> getCourseByName(@PathVariable String courseName) {
        Optional<Course> course = courseService.findByCourseName(courseName);
        return course.map(value -> ResponseEntity.status(HttpStatus.OK).body(value))
                .orElseGet(() -> ResponseEntity.status(HttpStatus.NOT_FOUND).build());
    }
}
