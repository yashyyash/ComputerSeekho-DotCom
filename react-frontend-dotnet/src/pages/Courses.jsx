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
              src={course.coursePhotoUrl || "/default-course.jpg"}
              alt={course.courseName}
              className="course-image"
              onError={(e) => {
                e.target.src = "/default-course.jpg";
              }}
            />

            <div className="course-info">
              <h3 className="course-title">{course.courseName}</h3>
              <ul className="course-details-list">
                <li>
                  <span>💰 Fee:</span> ₹{course.courseFee}
                </li>
                <li>
                  <span>⏳ Duration:</span> {course.durationMonths} Months
                </li>
              </ul>

              <details className="syllabus-toggle">
                <summary>📚 View Syllabus</summary>
                <div className="syllabus-content">{course.syllabus}</div>
              </details>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Courses;
