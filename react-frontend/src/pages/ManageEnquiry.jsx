import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import "./ManageEnquiry.css";
import { useLocation, useNavigate } from "react-router-dom";

const ManageEnquiry = () => {
  const navigate = useNavigate();
  const formRef = useRef(null);

  const [enquiries, setEnquiries] = useState([]);
  const [staffSearch, setStaffSearch] = useState("");
  const [courses, setCourses] = useState([]);
  const [staffList, setStaffList] = useState([]);

  const [formData, setFormData] = useState({
    enquiryId: null,
    enquirerName: "",
    enquirerAddress: "",
    enquirerMobile: "",
    enquirerEmailId: "",
    enquiryDate: "",
    enquirerQuery: "",
    courseName: "",
    studentName: "",
    enquiryCounter: 0,
    followUpDate: "",
    enquiryIsActive: true,
    staffId: null
  });

  const location = useLocation();

  // Fetch Courses and Staff for dropdowns
  const fetchDropdownData = async () => {
    try {
      const [courseRes, staffRes] = await Promise.all([
        axios.get("http://localhost:8080/api/course"),
        axios.get("http://localhost:8080/api/staff")
      ]);
      setCourses(Array.isArray(courseRes.data) ? courseRes.data : []);
      setStaffList(Array.isArray(staffRes.data) ? staffRes.data : []);
    } catch (err) {
      console.error("Error fetching dropdown data", err);
    }
  };

  // Fetch Enquiries
  const fetchEnquiries = async () => {
    try {
      const res = await axios.get("http://localhost:8080/api/enquiry");
      setEnquiries(Array.isArray(res.data) ? res.data : []);
    } catch (error) {
      console.error("Error fetching enquiries", error);
    }
  };

  // Fetch By Staff ID
  const fetchByStaffId = async () => {
    if (!staffSearch.trim()) {
      fetchEnquiries();
      return;
    }
    try {
      const res = await axios.get(
        `http://localhost:8080/api/enquiry/getByStaffId/${staffSearch}`
      );
      setEnquiries(Array.isArray(res.data) ? res.data : []);
    } catch (error) {
      console.error("Error fetching by staffId", error);
      alert("No enquiries found for this staff ID.");
    }
  };

  // On page load
  useEffect(() => {
    fetchDropdownData();
    fetchEnquiries();
  }, []);

  // If edit data is passed from another page
  useEffect(() => {
    if (location.state?.editData) {
      const data = location.state.editData;
      setFormData({
        enquiryId: data.enquiryId || null,
        enquirerName: data.enquirerName || "",
        enquirerAddress: data.enquirerAddress || "",
        enquirerMobile: data.enquirerMobile || "",
        enquirerEmailId: data.enquirerEmailId || "",
        enquiryDate: data.enquiryDate || "",
        enquirerQuery: data.enquirerQuery || "",
        courseName: data.courseName || data.course?.courseName || "",
        studentName: data.studentName || "",
        enquiryCounter: data.enquiryCounter || 0,
        followUpDate: data.followUpDate || "",
        enquiryIsActive: data.enquiryIsActive ?? true,
        staffId: data.staff?.staffId || null
      });
      formRef.current.scrollIntoView({ behavior: "smooth" });
    }
  }, [location.state]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    let updatedForm = { ...formData };

    // Attach staff object if staffId exists
    if (formData.staffId) {
      updatedForm.staff = { staffId: formData.staffId };
      delete updatedForm.staffId;
    }

if (formData.enquiryDate) {
  const baseDate = new Date(formData.enquiryDate);
  baseDate.setDate(baseDate.getDate() + 3);
  updatedForm.followUpDate = baseDate.toISOString().split("T")[0];
}

if (formData.enquiryId) {
  updatedForm.enquiryCounter = (formData.enquiryCounter || 0) + 1;
} else {
  updatedForm.enquiryCounter = 0; // first time
}


    try {
      if (formData.enquiryId) {
        await axios.put("http://localhost:8080/api/enquiry", updatedForm);
        alert("Enquiry Updated");
      } else {
        await axios.post("http://localhost:8080/api/enquiry", updatedForm);
        alert("Enquiry Added");
      }
      resetForm();
      fetchEnquiries();
    } catch (error) {
      console.error("Error saving enquiry", error);
    }
  };

  const handleEdit = (data) => {
    setFormData({
      enquiryId: data.enquiryId || null,
      enquirerName: data.enquirerName || "",
      enquirerAddress: data.enquirerAddress || "",
      enquirerMobile: data.enquirerMobile || "",
      enquirerEmailId: data.enquirerEmailId || "",
      enquirerQuery: data.enquirerQuery || "",
      courseName: data.courseName || data.course?.courseName || "",
      studentName: data.studentName || "",
      enquiryCounter: data.enquiryCounter || 0,
      followUpDate: data.followUpDate || "",
      enquiryIsActive: data.enquiryIsActive ?? true,
      enquiryDate: data.enquiryDate || "",
      staffId: data.staff?.staffId || null
    });
    formRef.current.scrollIntoView({ behavior: "smooth" });
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this enquiry?")) {
      try {
        await axios.delete(`http://localhost:8080/api/enquiry/${id}`);
        fetchEnquiries();
      } catch (error) {
        console.error("Error deleting enquiry", error);
      }
    }
  };

  const handleCloseEnquiry = async (id) => {
    const reason = prompt("Enter closure reason:");
    if (!reason) return;
    try {
      await axios.put(
        `http://localhost:8080/api/enquiry/deactivate/${id}`,
        reason,
        { headers: { "Content-Type": "text/plain" } }
      );
      alert("Enquiry closed successfully");
      fetchEnquiries();
    } catch (error) {
      console.error("Error closing enquiry", error);
    }
  };

  const resetForm = () => {
    setFormData({
      enquiryId: null,
      enquirerName: "",
      enquirerAddress: "",
      enquirerMobile: "",
      enquirerEmailId: "",
      enquiryDate: "",
      enquirerQuery: "",
      courseName: "",
      studentName: "",
      enquiryCounter: 0,
      followUpDate: "",
      enquiryIsActive: true,
      staffId: null
    });
  };

  return (
    <div className="enquiry-container">
      <h2 ref={formRef}>{formData.enquiryId ? "Edit Enquiry" : "Add Enquiry"}</h2>

      <div className="search-bar">
        <input
          type="text"
          placeholder="Search by Staff ID"
          value={staffSearch}
          onChange={(e) => setStaffSearch(e.target.value)}
        />
        <button onClick={fetchByStaffId}>Search</button>
        <button onClick={fetchEnquiries}>Reset</button>
      </div>

      <form className="enquiry-form" onSubmit={handleSubmit}>
        <input type="text" name="enquirerName" placeholder="Enquirer Name" value={formData.enquirerName} onChange={handleChange} required />
        <input type="text" name="enquirerAddress" placeholder="Enquirer Address" value={formData.enquirerAddress} onChange={handleChange} />
        <input type="text" name="enquirerMobile" placeholder="Mobile" value={formData.enquirerMobile} onChange={handleChange} required />
        <input type="email" name="enquirerEmailId" placeholder="Email" value={formData.enquirerEmailId} onChange={handleChange} />
        <input type="date" name="enquiryDate" value={formData.enquiryDate} onChange={handleChange} required />
        <textarea name="enquirerQuery" placeholder="Enquirer Query" value={formData.enquirerQuery} onChange={handleChange}></textarea>

        {/* Dropdown for Course */}
        <select name="courseName" value={formData.courseName} onChange={handleChange} required>
          <option value="">Select Course</option>
          {courses.map(course => (
            <option key={course.courseId} value={course.courseName}>
              {course.courseName}
            </option>
          ))}
        </select>

        {/* Dropdown for Staff */}
        <select name="staffId" value={formData.staffId || ""} onChange={handleChange}>
          <option value="">Assign Staff</option>
          {staffList.map(staff => (
            <option key={staff.staffId} value={staff.staffId}>
              {staff.staffName} ({staff.staffRole})
            </option>
          ))}
        </select>

        <input type="text" name="studentName" placeholder="Student Name" value={formData.studentName} onChange={handleChange} />
        <input type="number" name="enquiryCounter" placeholder="Follow-Up Count" value={formData.enquiryCounter} disabled />
        <input type="date" name="followUpDate" value={formData.followUpDate} disabled />
        <label>
          Active: <input type="checkbox" name="enquiryIsActive" checked={formData.enquiryIsActive} onChange={handleChange} />
        </label>
        <button type="submit">{formData.enquiryId ? "Update" : "Add"}</button>
        {formData.enquiryId && <button type="button" onClick={resetForm}>Cancel</button>}
      </form>

      <h3>All Enquiries</h3>
      <table className="enquiry-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Mobile</th>
            <th>Course</th>
            <th>Staff</th>
            <th>Follow-Up</th>
            <th>Active</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {enquiries.length > 0 ? (
            enquiries.map((enq) => (
              <tr key={enq.enquiryId}>
                <td>{enq.enquiryId}</td>
                <td>{enq.enquirerName}</td>
                <td>{enq.enquirerMobile}</td>
                <td>{enq.courseName || enq.course?.courseName || "N/A"}</td>
                <td>{enq.staff?.staffName || "Unassigned"}</td>
                <td>{enq.enquiryCounter}</td>
                <td>{enq.enquiryIsActive ? "✅" : "❌"}</td>
                <td>
                  <button 
                    onClick={() => navigate("/register", { state: { enquiryData: enq } })} 
                    className="register-btn"
                  >
                    Register
                  </button>
                  <button onClick={() => handleEdit(enq)}>Edit</button>
                  <button onClick={() => handleDelete(enq.enquiryId)} className="delete-btn">Delete</button>
                  {enq.enquiryIsActive && (
                    <button onClick={() => handleCloseEnquiry(enq.enquiryId)} className="close-btn">
                      Close
                    </button>
                  )}
                </td>
              </tr>
            ))
          ) : (
            <tr><td colSpan="8">No Enquiries Found</td></tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ManageEnquiry;
