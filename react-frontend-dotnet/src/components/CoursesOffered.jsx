import React, { useEffect, useState, useCallback } from "react";
import axios from "axios";
import "./CoursesOffered.css";
import { useNavigate } from "react-router-dom";

// Memoized course card
const CourseCard = React.memo(({ course, onClick }) => {
  const imageUrl = course.coursePhotoUrl || "/default-course.jpg";

  return (
    <div className="course-card" onClick={() => onClick(course.courseId)}>
      <div className="course-image-wrapper">
        <img
          src={imageUrl}
          alt={course.courseName || "N/A"}
          className="course-image"
          onError={(e) => (e.target.src = "/default-course.jpg")}
          loading="lazy"
        />
      </div>

      <div className="course-info"><br></br>
      <h6>Course Name:-</h6>
        <h3 className="course-title">{course.courseName || "N/A"}</h3>
        {/* <ul className="course-details-list">
          <li>💰 Fee: ₹{course.courseFee || "N/A"}</li>
          <li>⏳ Duration: {course.durationMonths || "N/A"} Months</li>
        </ul> */}

        {/* {course.syllabus && (
          <details className="syllabus-toggle">
            <summary>📚 View Syllabus</summary>
            <div className="syllabus-content">{course.syllabus}</div>
          </details>
        )} */}
      </div>
    </div>
  );
});

const CoursesOffered = () => {
  const [courses, setCourses] = useState([]);
  const navigate = useNavigate();

  const handleRedirect = useCallback(
    (courseId) => {
      navigate(`/courses/${courseId}`);
    },
    [navigate]
  );

  useEffect(() => {
    axios
      .get("http://localhost:8080/api/course")
      .then((res) => setCourses(res.data || []))
      .catch((err) => console.error("Error fetching courses:", err));
  }, []);

  if (courses.length === 0) {
    return <p style={{ textAlign: "center", marginTop: "50px" }}>No courses available.</p>;
  }

  return (
    <div className="courses-container">
      <h2 className="courses-heading">All Courses</h2>
      <div className="courses-grid">
        {courses.map((course) => (
          <CourseCard key={course.courseId} course={course} onClick={handleRedirect} />
        ))}
      </div>
    </div>
  );
};

export default CoursesOffered;
