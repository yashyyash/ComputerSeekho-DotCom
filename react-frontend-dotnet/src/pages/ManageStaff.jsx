// src/pages/ManageStaff.jsx
import React, { useState, useEffect } from "react";
import axios from "axios";
import "./ManageStaff.css";

const API_URL = "http://localhost:8080/api/staff";

const ManageStaff = () => {
  const [staffList, setStaffList] = useState([]);
  const [formData, setFormData] = useState({
    StaffId: "",
    StaffName: "",
    StaffPhotoUrl: "",
    StaffMobile: "",
    StaffEmail: "",
    StaffUsername: "",
    StaffPassword: "",
    StaffRoleId: "",
    IsActive: true
  });
  const [isEditing, setIsEditing] = useState(false);

  useEffect(() => {
    fetchStaff();
  }, []);

  const fetchStaff = async () => {
    try {
      const response = await axios.get(API_URL);
      console.log("Fetched staff:", response.data);

      // If backend uses different field names, fix here
      const fixedData = response.data.map((staff, index) => ({
        StaffId: staff.StaffId || staff.id || index + 1,
        StaffName: staff.StaffName || staff.name || "N/A",
        StaffPhotoUrl: staff.StaffPhotoUrl
          ? staff.StaffPhotoUrl.replace(/^public[\\\/]/, "").replace(/\\/g, "/")
          : "",
        StaffMobile: staff.StaffMobile || staff.mobile || "N/A",
        StaffEmail: staff.StaffEmail || staff.email || "N/A",
        StaffUsername: staff.StaffUsername || staff.username || "N/A",
        StaffRoleId: staff.StaffRoleId || staff.roleId || "N/A",
        IsActive: staff.IsActive !== undefined ? staff.IsActive : true
      }));
      setStaffList(fixedData);
    } catch (error) {
      console.error("Error fetching staff:", error);
      setStaffList([]); // Ensure table renders empty state
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEditing) {
        const updateData = { ...formData };
        delete updateData.StaffPassword;
        await axios.put(`${API_URL}/${formData.StaffId}`, updateData);
        alert("Staff updated successfully");
      } else {
        const postData = { ...formData };
        await axios.post(API_URL, postData);
        alert("Staff added successfully");
      }
      resetForm();
      fetchStaff();
    } catch (error) {
      console.error("Error saving staff:", error);
      alert("Error saving staff");
    }
  };

  const handleEdit = (staff) => {
    setFormData({
      ...staff,
      StaffPassword: ""
    });
    setIsEditing(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this staff?")) {
      try {
        await axios.delete(`${API_URL}/${id}`);
        alert("Staff deleted successfully");
        fetchStaff();
      } catch (error) {
        console.error("Error deleting staff:", error);
      }
    }
  };

  const resetForm = () => {
    setFormData({
      StaffId: "",
      StaffName: "",
      StaffPhotoUrl: "",
      StaffMobile: "",
      StaffEmail: "",
      StaffUsername: "",
      StaffPassword: "",
      StaffRoleId: "",
      IsActive: true
    });
    setIsEditing(false);
  };

  return (
    <div className="manage-staff-container">
      <h2>{isEditing ? "Edit Staff" : "Add Staff"}</h2>
      <form onSubmit={handleSubmit} className="staff-form">
        <input
          type="text"
          name="StaffName"
          placeholder="Name"
          value={formData.StaffName}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="StaffPhotoUrl"
          placeholder="Photo filename in public folder"
          value={formData.StaffPhotoUrl}
          onChange={handleChange}
        />
        <input
          type="text"
          name="StaffMobile"
          placeholder="Mobile"
          value={formData.StaffMobile}
          onChange={handleChange}
          required
        />
        <input
          type="email"
          name="StaffEmail"
          placeholder="Email"
          value={formData.StaffEmail}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="StaffUsername"
          placeholder="Username"
          value={formData.StaffUsername}
          onChange={handleChange}
          required
        />
        {!isEditing && (
          <input
            type="password"
            name="StaffPassword"
            placeholder="Password"
            value={formData.StaffPassword}
            onChange={handleChange}
            required
          />
        )}
        <input
          type="number"
          name="StaffRoleId"
          placeholder="Role ID"
          value={formData.StaffRoleId}
          onChange={handleChange}
          required
        />
        <button type="submit">{isEditing ? "Update" : "Add"}</button>
        {isEditing && (
          <button type="button" onClick={resetForm} className="cancel-btn">
            Cancel
          </button>
        )}
      </form>

      <h2>Staff List</h2>

      {/* Debug output */}
      <pre>{JSON.stringify(staffList, null, 2)}</pre>

      <table className="staff-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Photo</th>
            <th>Name</th>
            <th>Mobile</th>
            <th>Email</th>
            <th>Username</th>
            <th>Role ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {staffList.length === 0 ? (
            <tr>
              <td colSpan="8">No staff found</td>
            </tr>
          ) : (
            staffList.map((staff) => (
              <tr key={staff.StaffId}>
                <td>{staff.StaffId}</td>
                <td>
                  {staff.StaffPhotoUrl ? (
                    <img
                      src={`/${staff.StaffPhotoUrl}`}
                      alt={staff.StaffName}
                      className="staff-photo"
                      onError={(e) =>
                        (e.currentTarget.src = "/fallback-image.jpg")
                      }
                    />
                  ) : (
                    "No photo"
                  )}
                </td>
                <td>{staff.StaffName}</td>
                <td>{staff.StaffMobile}</td>
                <td>{staff.StaffEmail}</td>
                <td>{staff.StaffUsername}</td>
                <td>{staff.StaffRoleId}</td>
                <td>
                  <button onClick={() => handleEdit(staff)}>Edit</button>
                  <button
                    className="delete-btn"
                    onClick={() => handleDelete(staff.StaffId)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ManageStaff;
