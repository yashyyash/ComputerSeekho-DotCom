import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./AddBatchPlacement.css";

const AddBatchPlacement = () => {
  const navigate = useNavigate();
  const formRef = useRef(null);

  const [formData, setFormData] = useState({
    batchId: null,
    batchName: "",
    batchPhotoUrl: "",
    startDate: "",
    endDate: "",
    courseId: "", 
  });

  const [batches, setBatches] = useState([]);
  const [courses, setCourses] = useState([]);

  // Fetch batches
  const fetchBatches = async () => {
    try {
      const res = await axios.get("http://localhost:8080/api/batch");
      setBatches(res.data);
    } catch (err) {
      console.error("Failed to fetch batches:", err);
    }
  };

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
    fetchBatches();
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

    // Build payload exactly matching BatchDto
    const payload = {
      BatchId: formData.batchId || 0, // 0 or omit for POST
      BatchName: formData.batchName,
      BatchPhotoUrl: formData.batchPhotoUrl.trim(),
      StartDate: formData.startDate,
      EndDate: formData.endDate,
      CourseId: parseInt(formData.courseId),
    };

    try {
      if (formData.batchId) {
        // PUT requires id in URL
        await axios.put(`http://localhost:8080/api/batch/${formData.batchId}`, payload);
        alert("Batch updated successfully!");
      } else {
        await axios.post("http://localhost:8080/api/batch", payload);
        alert("Batch added successfully!");
      }

      setFormData({
        batchId: null,
        batchName: "",
        batchPhotoUrl: "",
        startDate: "",
        endDate: "",
        courseId: "",
      });

      fetchBatches();
    } catch (err) {
      console.error("Save failed:", err);
      alert("Failed to save batch. Check console for details.");
    }
  };

  const handleEdit = (batch) => {
    setFormData({
      batchId: batch.batchId,
      batchName: batch.batchName,
      batchPhotoUrl: batch.batchPhotoUrl,
      startDate: batch.startDate ? batch.startDate.split("T")[0] : "",
      endDate: batch.endDate ? batch.endDate.split("T")[0] : "",
      courseId: batch.courseId.toString(),
    });

    setTimeout(() => {
      formRef.current.scrollIntoView({ behavior: "smooth" });
    }, 100);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this batch?")) {
      try {
        await axios.delete(`http://localhost:8080/api/batch/${id}`);
        alert("Batch deleted successfully!");
        fetchBatches();
      } catch (err) {
        console.error("Delete failed:", err);
        alert("Failed to delete batch.");
      }
    }
  };

  return (
    <div className="add-placement-container">
      <div ref={formRef}>
        <h2>{formData.batchId ? "Edit" : "Add"} Batch</h2>
        <form className="add-placement-form" onSubmit={handleSubmit}>
          <label>
            Batch Name:
            <input
              type="text"
              name="batchName"
              value={formData.batchName}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Batch Photo URL:
            <input
              type="text"
              name="batchPhotoUrl"
              placeholder="/public/batchwisePlacement/file.jpg"
              value={formData.batchPhotoUrl}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Start Date:
            <input
              type="date"
              name="startDate"
              value={formData.startDate}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            End Date:
            <input
              type="date"
              name="endDate"
              value={formData.endDate}
              onChange={handleChange}
              required
            />
          </label>

          <label>
            Course:
            <select
              name="courseId"
              value={formData.courseId}
              onChange={handleChange}
              required
            >
              <option value="">--Select Course--</option>
              {courses.map((c) => (
                <option key={c.courseId} value={c.courseId}>
                  {c.courseName}
                </option>
              ))}
            </select>
          </label>

          <button type="submit">{formData.batchId ? "Update" : "Add"} Batch</button>
        </form>
      </div>

      <h3>All Batches</h3>
      <table className="placement-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Batch Name</th>
            <th>Photo</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {batches.map((batch) => (
            <tr key={batch.batchId}>
              <td>{batch.batchId}</td>
              <td>{batch.batchName}</td>
              <td>
                <img
                  src={batch.batchPhotoUrl}
                  alt="Batch"
                  style={{ width: "50px", height: "50px", objectFit: "cover" }}
                />
              </td>
              <td>
                <button onClick={() => handleEdit(batch)}>Edit</button>
                <button onClick={() => handleDelete(batch.batchId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AddBatchPlacement;
