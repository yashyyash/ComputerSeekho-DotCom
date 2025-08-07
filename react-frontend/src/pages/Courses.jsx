import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Courses.css";

const Courses = () => {
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:8080/api/course")
      .then((res) => setCourses(res.data))
      .catch((err) => console.error("Error fetching courses:", err));
  }, []);

  return (
    <div className="courses-container">
      <h2 className="courses-heading">All Courses</h2>
      <div className="courses-grid">
        {courses.map((course) => (
          <div className="course-card" key={course.courseId}>
            <img
              src={course.coverPhoto}
              alt={course.courseName}
              className="course-image"
              onError={(e) => {
                e.target.src = "/default-course.jpg"; // fallback
              }}
            />
            <div className="course-info">
              <h3>{course.courseName}</h3>
              <p><strong>Fee:</strong> ₹{course.courseFee}</p>
              <p><strong>Duration:</strong> {course.courseDuration} Months</p>
              <p><strong>Age Group:</strong> {course.ageGrpType}</p>
              <p><strong>Status:</strong> {course.courseIsActive ? "Active" : "Inactive"}</p>
              <p className="course-description">{course.courseDescription}</p>
              <details>
                <summary>View Syllabus</summary>
                <p>{course.courseSyllabus}</p>
              </details>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Courses;
