import React, { useEffect, useState } from "react";
import axios from "axios";
import "./AddCourse.css";

const AddCourse = () => {
  const [courses, setCourses] = useState([]);
  const [formData, setFormData] = useState({
    courseId: null,
    courseName: "",
    courseFee: "",
    coursePhotoUrl: "",
    durationMonths: "",
    syllabus: ""
  });
  const [message, setMessage] = useState("");

  // Fetch courses
  const fetchCourses = async () => {
    try {
      const res = await axios.get("http://localhost:8080/api/course");
      setCourses(res.data);
    } catch (err) {
      console.error("Failed to fetch courses:", err);
    }
  };

  useEffect(() => {
    fetchCourses();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const payload = {
      ...formData,
      courseFee: parseFloat(formData.courseFee) || 0,
      durationMonths: parseInt(formData.durationMonths) || 0
    };

    try {
      if (formData.courseId) {
        await axios.put(`http://localhost:8080/api/course/${formData.courseId}`, payload);
        setMessage("Course updated successfully!");
      } else {
        await axios.post("http://localhost:8080/api/course", payload);
        setMessage("Course added successfully!");
      }
      setFormData({
        courseId: null,
        courseName: "",
        courseFee: "",
        coursePhotoUrl: "",
        durationMonths: "",
        syllabus: ""
      });
      fetchCourses();
      setTimeout(() => setMessage(""), 3000);
    } catch (err) {
      console.error("Failed to save course:", err);
      setMessage("Failed to save course.");
    }
  };

  const handleEdit = (course) => {
    setFormData({
      courseId: course.courseId,
      courseName: course.courseName,
      courseFee: course.courseFee,
      coursePhotoUrl: course.coursePhotoUrl,
      durationMonths: course.durationMonths,
      syllabus: course.syllabus
    });
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this course?")) {
      try {
        await axios.delete(`http://localhost:8080/api/course/${id}`);
        setMessage("Course deleted successfully!");
        fetchCourses();
        setTimeout(() => setMessage(""), 3000);
      } catch (err) {
        console.error("Failed to delete course:", err);
        setMessage("Failed to delete course.");
      }
    }
  };

  return (
    <div className="course-container">
      <h2>{formData.courseId ? "Edit Course" : "Add Course"}</h2>
      {message && <p className="message">{message}</p>}
      <form onSubmit={handleSubmit} className="course-form">
        <input
          type="text"
          name="courseName"
          value={formData.courseName}
          placeholder="Course Name"
          onChange={handleChange}
          required
        />
        <input
          type="number"
          name="courseFee"
          value={formData.courseFee}
          placeholder="Course Fee"
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="coursePhotoUrl"
          value={formData.coursePhotoUrl}
          placeholder="Course Photo URL"
          onChange={handleChange}
        />
        <input
          type="number"
          name="durationMonths"
          value={formData.durationMonths}
          placeholder="Duration (Months)"
          onChange={handleChange}
          required
        />
        <textarea
          name="syllabus"
          value={formData.syllabus}
          placeholder="Syllabus"
          onChange={handleChange}
        />
        <button type="submit">{formData.courseId ? "Update" : "Add"} Course</button>
      </form>

      <h3>All Courses</h3>
      <ul className="course-list">
        {courses.map((course) => (
          <li key={course.courseId}>
            <strong>{course.courseName}</strong> - ₹{course.courseFee} - {course.durationMonths} months
            <div className="actions">
              <button onClick={() => handleEdit(course)}>Edit</button>
              <button onClick={() => handleDelete(course.courseId)}>Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AddCourse;
