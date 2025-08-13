import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import "./AddCourse.css";

const AddCourse = () => {
  const [formData, setFormData] = useState({
    courseId: null,
    courseName: "",
    courseFee: "",
    coursePhotoUrl: "",
    durationMonths: "",
    syllabus: "",
  });

  const [courses, setCourses] = useState([]);
  const formRef = useRef(null);

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
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      courseName: formData.courseName,
      courseFee: parseFloat(formData.courseFee) || 0,
      coursePhotoUrl: formData.coursePhotoUrl,
      durationMonths: parseInt(formData.durationMonths) || 0,
      syllabus: formData.syllabus,
    };

    try {
      if (formData.courseId) {
        await axios.put(`http://localhost:8080/api/course/${formData.courseId}`, payload);
        alert("Course updated successfully!");
      } else {
        await axios.post("http://localhost:8080/api/course", payload);
        alert("Course added successfully!");
      }

      setFormData({
        courseId: null,
        courseName: "",
        courseFee: "",
        coursePhotoUrl: "",
        durationMonths: "",
        syllabus: "",
      });

      fetchCourses();
    } catch (err) {
      console.error("Save failed:", err);
      alert("Failed to save course.");
    }
  };

  const handleEdit = (course) => {
    setFormData({ ...course });

    setTimeout(() => {
      formRef.current.scrollIntoView({ behavior: "smooth" });
    }, 100);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this course?")) {
      try {
        await axios.delete(`http://localhost:8080/api/course/${id}`);
        alert("Course deleted successfully!");
        fetchCourses();
      } catch (err) {
        console.error("Delete failed:", err);
        alert("Failed to delete course.");
      }
    }
  };

  return (
    <div className="add-course-container">
      {/* Form Section */}
      <div ref={formRef}>
        <h2 className="form-heading">{formData.courseId ? "Edit" : "Add"} Course</h2>
        <form className="add-course-form" onSubmit={handleSubmit}>
          <label>
            Course Name:
            <input
              type="text"
              name="courseName"
              value={formData.courseName}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Duration (Months):
            <input
              type="number"
              name="durationMonths"
              value={formData.durationMonths}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Fee:
            <input
              type="number"
              name="courseFee"
              value={formData.courseFee}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Cover Photo URL:
            <input
              type="text"
              name="coursePhotoUrl"
              value={formData.coursePhotoUrl}
              onChange={handleChange}
            />
          </label>

          <label>
            Syllabus:
            <textarea
              name="syllabus"
              value={formData.syllabus}
              onChange={handleChange}
            />
          </label>

          <button type="submit" className="submit-btn">
            {formData.courseId ? "Update Course" : "Add Course"}
          </button>
        </form>
      </div>

      {/* Courses Table */}
      <h3 className="form-heading">Existing Courses</h3>
      <table className="course-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Fee</th>
            <th>Duration</th>
            <th>Cover</th>
            <th>Syllabus</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {courses.map((c) => (
            <tr key={c.courseId}>
              <td>{c.courseId}</td>
              <td>{c.courseName}</td>
              <td>₹{c.courseFee}</td>
              <td>{c.durationMonths} Months</td>
              <td>
                <img
                  src={c.coursePhotoUrl}
                  alt="cover"
                  style={{ width: "50px", height: "50px", objectFit: "cover" }}
                />
              </td>
              <td>{c.syllabus}</td>
              <td>
                <button className="update-btn" onClick={() => handleEdit(c)}>
                  Edit
                </button>
                <button className="delete-btn" onClick={() => handleDelete(c.courseId)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AddCourse;
