import React, { useEffect, useState } from "react";
import axios from "axios";
import "./CoursesOffered.css";
import { useNavigate } from "react-router-dom";

const CoursesOffered = () => {
  const [courses, setCourses] = useState([]);
  const navigate = useNavigate();

  const handleRedirect = () => {
    navigate("/courses");
  };

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
        <a onClick={handleRedirect}>
        {courses.map((course) => (
          <div className="course-card" key={course.courseId}>
            <img
              src={course.coverPhoto}
              alt={course.courseName}
              className="course-image"
              onError={(e) => {
                e.target.src = "/default-course.jpg"; // fallback image
              }}
            />
            <div className="course-info">
              <h3>{course.courseName}</h3>
              <p><strong>Duration:</strong> {course.courseDuration} Months</p>
              {/* <p><strong>Fee:</strong> ₹{course.courseFee}</p>
              <p><strong>Age Group:</strong> {course.ageGrpType}</p>
              <p><strong>Status:</strong> {course.courseIsActive ? "Active" : "Inactive"}</p>
              <p className="course-description">{course.courseDescription}</p>
              <details>
                <summary>View Syllabus</summary>
                <p>{course.courseSyllabus}</p>
              </details> */}
            </div>
          </div>
          
        ))}
        </a>
        
      </div>
      
    </div>
  );
};
// Export the component to be used in other parts of the app
export default CoursesOffered;
