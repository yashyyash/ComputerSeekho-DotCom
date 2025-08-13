import React, { useEffect, useState } from "react";
import axios from "axios";
import "./AddFaculty.css";

const API_URL = "http://localhost:8080/api/faculty";

const AddFaculty = () => {
  const [faculties, setFaculties] = useState([]);
  const [form, setForm] = useState({
    facultyName: "",
    teachingSubject: "",
    photoUrl: ""
  });
  const [editingId, setEditingId] = useState(null);

  // Fetch all faculty
  const fetchFaculties = async () => {
    try {
      const response = await axios.get(API_URL);
      setFaculties(response.data || []);
    } catch (error) {
      console.error("Error fetching faculty:", error);
    }
  };

  useEffect(() => {
    fetchFaculties();
  }, []);

  // Handle input changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  // Add or update
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingId) {
        await axios.put(`${API_URL}/${editingId}`, form);
      } else {
        await axios.post(API_URL, form);
      }
      setForm({ facultyName: "", teachingSubject: "", photoUrl: "" });
      setEditingId(null);
      fetchFaculties();
    } catch (error) {
      console.error("Error saving faculty:", error);
    }
  };

  // Edit
  const handleEdit = (faculty) => {
    setForm({
      facultyName: faculty.facultyName || "",
      teachingSubject: faculty.teachingSubject || "",
      photoUrl: faculty.photoUrl || ""
    });
    setEditingId(faculty.facultyId);
  };

  // Delete
  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this faculty?")) {
      try {
        await axios.delete(`${API_URL}/${id}`);
        fetchFaculties();
      } catch (error) {
        console.error("Error deleting faculty:", error);
      }
    }
  };

  return (
    <div className="faculty-container">
      <h2>{editingId ? "Edit Faculty" : "Add Faculty"}</h2>

      <form onSubmit={handleSubmit} className="faculty-form">
        <input
          type="text"
          name="facultyName"
          placeholder="Faculty Name"
          value={form.facultyName}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="teachingSubject"
          placeholder="Teaching Subject"
          value={form.teachingSubject}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="photoUrl"
          placeholder="Photo URL"
          value={form.photoUrl}
          onChange={handleChange}
        />
        <button type="submit">{editingId ? "Update" : "Add"} Faculty</button>
      </form>

      <div className="faculty-list">
        {faculties.map((faculty) => (
          <div key={faculty.facultyId} className="faculty-card">
            <div className="faculty-photo">
              <img
                src={
                  faculty.photoUrl
                    ? faculty.photoUrl.startsWith("http")
                      ? faculty.photoUrl
                      : `/${faculty.photoUrl.replaceAll("\\", "/")}`
                    : "https://placehold.co/150x150?text=No+Image"
                }
                alt={faculty.facultyName}
              />
            </div>
            <div className="faculty-info">
              <h3>{faculty.facultyName}</h3>
              <p>
                <strong>Subject:</strong> {faculty.teachingSubject}
              </p>
            </div>
            <div className="faculty-actions">
              <button onClick={() => handleEdit(faculty)}>Edit</button>
              <button onClick={() => handleDelete(faculty.facultyId)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default AddFaculty;
