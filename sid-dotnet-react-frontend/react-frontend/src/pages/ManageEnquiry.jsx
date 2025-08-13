import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import "./ManageEnquiry.css";
import { useLocation, useNavigate } from "react-router-dom";
import { FaSearch, FaPlus, FaTimes, FaEdit, FaTrash, FaUserCheck } from "react-icons/fa";

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
    staffId: 1
  });

  const location = useLocation();

  const fetchDropdownData = async () => {
    try {
      // const [courseRes, staffRes] = await Promise.all([
      //   axios.get("https://localhost:7094/api/Course"),
      //   axios.get("http://localhost:8080/api/staff")
      // ]);
      const [courseRes,staffRes] = await Promise.all([
        axios.get("https://localhost:7094/api/Course"),
        axios.get("https://localhost:7094/api/Staff")
      ]);
      setCourses(Array.isArray(courseRes.data) ? courseRes.data : []);
      setStaffList(Array.isArray(staffRes.data) ? staffRes.data : []);
    } catch (err) {
      console.error("Error fetching dropdown data", err);
    }
  };

  const fetchEnquiries = async () => {
    try {
      const res = await axios.get("https://localhost:7094/api/Enquiry");
      setEnquiries(Array.isArray(res.data) ? res.data : []);
    } catch (error) {
      console.error("Error fetching enquiries", error);
    }
  };

  const fetchByStaffId = async () => {
    if (!staffSearch.trim()) {
      fetchEnquiries();
      return;
    }
    try {
      const res = await axios.get(
        `https://localhost:7094/api/Enquiry/GetByStaffId/${staffSearch}`
      );
      setEnquiries(Array.isArray(res.data) ? res.data : []);
    } catch (error) {
      console.error("Error fetching by staffId", error);
      alert("No enquiries found for this staff ID.");
    }
  };

  useEffect(() => {
    fetchDropdownData();
    fetchEnquiries();
  }, []);

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
        staffId: data.staff?.staffId || 1
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

    if (formData.staffId) {
      updatedForm.staff = { staffId: formData.staffId };
      delete updatedForm.staffId;
    }

    if (formData.enquiryDate) {
      const baseDate = new Date(formData.enquiryDate);
      baseDate.setDate(baseDate.getDate() + 3);
      updatedForm.followUpDate = baseDate.toISOString().split("T")[0];
    }

    updatedForm.enquiryCounter = formData.enquiryId
      ? (formData.enquiryCounter || 0) + 1
      : 0;

    try {
      if (formData.enquiryId) {
        await axios.put("https://localhost:7094/api/Enquiry", updatedForm);
        alert("Enquiry Updated");
      } else {
        await axios.post("https://localhost:7094/api/Enquiry", updatedForm);
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
      staffId: data.staff?.staffId || 1
    });
    formRef.current.scrollIntoView({ behavior: "smooth" });
  };

  const handleDelete = async (id) => {
    if (window.confirm("Delete this enquiry?")) {
      try {
        await axios.delete(`https://localhost:7094/api/Enquiry/${id}`);
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
        `https://localhost:7094/api/Enquiry/Deactivate/${id}`,
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
      staffId: 1
    });
  };

 return (
  <div className="enquiry-container">
    <h2 ref={formRef} className="title">
      {formData.enquiryId ? "Edit Enquiry" : "Add Enquiry"}
    </h2>

    <div className="search-bar">
      <input
        type="text"
        placeholder="Search by Staff ID"
        value={staffSearch}
        onChange={(e) => setStaffSearch(e.target.value)}
      />
      <button onClick={fetchByStaffId}><FaSearch /> Search</button>
      <button onClick={fetchEnquiries}><FaTimes /> Reset</button>
    </div>

    <form className="enquiry-form" onSubmit={handleSubmit}>
  <div>
    <small>Enquirer Name:</small>
    <input
      type="text"
      name="enquirerName"
      placeholder="Enquirer Name"
      value={formData.enquirerName}
      onChange={handleChange}
      required
    />
  </div>

  <div>
    <small>Enquirer Address:</small>
    <input
      type="text"
      name="enquirerAddress"
      placeholder="Enquirer Address"
      value={formData.enquirerAddress}
      onChange={handleChange}
    />
  </div>

  <div>
    <small>Mobile:</small>
    <input
      type="text"
      name="enquirerMobile"
      placeholder="Mobile"
      value={formData.enquirerMobile}
      onChange={handleChange}
      required
    />
  </div>

  <div>
    <small>Email:</small>
    <input
      type="email"
      name="enquirerEmailId"
      placeholder="Email"
      value={formData.enquirerEmailId}
      onChange={handleChange}
    />
  </div>

  <div>
    <small>Enquiry Date:</small>
    <input
      type="date"
      name="enquiryDate"
      value={formData.enquiryDate}
      onChange={handleChange}
      required
    />
  </div>

  <div>
    <small>Enquirer Query:</small>
    <textarea
      name="enquirerQuery"
      placeholder="Enquirer Query"
      value={formData.enquirerQuery}
      onChange={handleChange}
    ></textarea>
  </div>

  <div>
    <small>Course:</small>
    <select
      name="courseName"
      value={formData.courseName}
      onChange={handleChange}
      required
    >
      <option value="">Select Course</option>
      {courses.map(course => (
        <option key={course.courseId} value={course.courseName}>
          {course.courseName}
        </option>
      ))}
    </select>
  </div>

  <div>
    <small>Assign Staff:</small>
    <select
      name="staffId"
      value={formData.staffId || 1}
      onChange={handleChange}
    >
      <option value="">Assign Staff</option>
      {staffList.map(staff => (
        <option key={staff.staffId} value={staff.staffId}>
          {staff.staffName} ({staff.staffRole})
        </option>
      ))}
    </select>
  </div>

  <div>
    <small>Student Name:</small>
    <input
      type="text"
      name="studentName"
      placeholder="Student Name"
      value={formData.studentName}
      onChange={handleChange}
    />
  </div>

  <div>
    <small>Follow-Up Count:</small>
    <input
      type="number"
      name="enquiryCounter"
      placeholder="Follow-Up Count"
      value={formData.enquiryCounter}
      disabled
    />
  </div>

  <div>
    <small>Follow-Up Date:</small>
    <input
      type="date"
      name="followUpDate"
      value={formData.followUpDate}
      disabled
    />
  </div>

  <div>
    <small>Active:</small>
    <input
      type="checkbox"
      name="enquiryIsActive"
      checked={formData.enquiryIsActive}
      onChange={handleChange}
    />
  </div>

  <button type="submit">
    {formData.enquiryId ? <FaEdit /> : <FaPlus />} {formData.enquiryId ? "Update" : "Add"}
  </button>

  {formData.enquiryId && (
    <button type="button" onClick={resetForm}>
      <FaTimes /> Cancel
    </button>
  )}
</form>


    <h3 className="subtitle">All Enquiries</h3>
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
                  <FaUserCheck />
                </button>
                <button onClick={() => handleEdit(enq)}><FaEdit /></button>
                <button onClick={() => handleDelete(enq.enquiryId)} className="delete-btn"><FaTrash /></button>
                {enq.enquiryIsActive && (
                  <button onClick={() => handleCloseEnquiry(enq.enquiryId)} className="close-btn"><FaTimes /></button>
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
