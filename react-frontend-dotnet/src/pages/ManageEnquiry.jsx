import React, { useState, useEffect } from "react";
import axios from "axios";
import "./ManageEnquiry.css";

const ManageEnquiry = () => {
  const [enquiries, setEnquiries] = useState([]);
  const [selectedEnquiryId, setSelectedEnquiryId] = useState(null);
  const [enquiry, setEnquiry] = useState({
    enquirerName: "",
    studentName: "",
    status: "Open",
    closureReason: null,
    createdAt: new Date().toISOString().slice(0, 16),
    enquiryQuery: "",
    studentEmail: "",
    inquirerEmail: "",
    studentGender: "",
    enquiryAddress: "",
    studentPhotoUrl: "",
    courseId: "",
    staffId: "",
    followUps: [],
  });

  // Fetch all enquiries
  const fetchEnquiries = async () => {
    try {
      const res = await axios.get("http://localhost:8080/api/enquiry");
      setEnquiries(res.data);
    } catch (err) {
      console.error("Error fetching enquiries", err);
    }
  };

  useEffect(() => {
    fetchEnquiries();
  }, []);

  // Fetch enquiry by ID for edit
  const editEnquiry = async (id) => {
    try {
      const res = await axios.get(`http://localhost:8080/api/enquiry/${id}`);
      const data = res.data;
      setSelectedEnquiryId(id);
      setEnquiry({
        enquirerName: data.enquirerName || "",
        studentName: data.studentName || "",
        status: data.status || "Open",
        closureReason: data.closureReason || null,
        createdAt: data.createdAt ? data.createdAt.slice(0, 16) : new Date().toISOString().slice(0, 16),
        enquiryQuery: data.enquiryQuery || "",
        studentEmail: data.studentEmail || "",
        inquirerEmail: data.inquirerEmail || "",
        studentGender: data.studentGender || "",
        enquiryAddress: data.enquiryAddress || "",
        studentPhotoUrl: data.studentPhotoUrl || "",
        courseId: data.courseId || "",
        staffId: data.staffId || "",
        followUps: data.followUps || [],
      });
      window.scrollTo({ top: 0, behavior: "smooth" });
    } catch (err) {
      console.error("Error fetching enquiry by ID", err);
    }
  };

  // Handle input change
  const handleChange = (e) => {
    const { name, value } = e.target;
    setEnquiry((prev) => ({ ...prev, [name]: value }));
  };

  // Add a new follow-up
  const addFollowUp = () => {
    setEnquiry((prev) => ({
      ...prev,
      followUps: [
        ...prev.followUps,
        {
          followupId: Date.now(),
          followupDate: new Date().toISOString().slice(0, 16),
          notes: "",
          status: "Pending",
        },
      ],
    }));
  };

  // Update follow-up fields
  const handleFollowUpChange = (index, e) => {
    const { name, value } = e.target;
    const updatedFollowUps = [...enquiry.followUps];
    updatedFollowUps[index][name] = value;
    setEnquiry((prev) => ({ ...prev, followUps: updatedFollowUps }));
  };

  // Delete follow-up
  const deleteFollowUp = (index) => {
    const updatedFollowUps = [...enquiry.followUps];
    updatedFollowUps.splice(index, 1);
    setEnquiry((prev) => ({ ...prev, followUps: updatedFollowUps }));
  };

  // Submit form (create or update)
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        enquirerName: enquiry.enquirerName,
        studentName: enquiry.studentName,
        status: Number(enquiry.status),
        closureReason: enquiry.closureReason || null,
        createdAt: enquiry.createdAt,
        enquiryQuery: enquiry.enquiryQuery || "",
        studentEmail: enquiry.studentEmail,
        inquirerEmail: enquiry.inquirerEmail,
        studentGender: enquiry.studentGender,
        enquiryAddress: enquiry.enquiryAddress,
        studentPhotoUrl: enquiry.studentPhotoUrl,
        courseId: Number(enquiry.courseId),
        staffId: Number(enquiry.staffId),
        followUps: enquiry.followUps.map(fu => ({
          followupId: fu.followupId,
          followupDate: fu.followupDate,
          notes: fu.notes,
          status: fu.status,
        }))
      };

      if (selectedEnquiryId) {
        await axios.put(`http://localhost:8080/api/enquiry/${selectedEnquiryId}`, payload);
        alert("Enquiry updated successfully!");
      } else {
        await axios.post("http://localhost:8080/api/enquiry", payload);
        alert("Enquiry created successfully!");
      }

      setSelectedEnquiryId(null);
      setEnquiry({
        enquirerName: "",
        studentName: "",
        status: "Open",
        closureReason: null,
        createdAt: new Date().toISOString().slice(0, 16),
        enquiryQuery: "",
        studentEmail: "",
        inquirerEmail: "",
        studentGender: "",
        enquiryAddress: "",
        studentPhotoUrl: "",
        courseId: "",
        staffId: "",
        followUps: [],
      });

      fetchEnquiries();
    } catch (error) {
      console.error("Error saving enquiry", error);
      alert("Error saving enquiry. Check console for details.");
    }
  };

  // Delete enquiry
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this enquiry?")) return;
    try {
      await axios.delete(`http://localhost:8080/api/enquiry/${id}`);
      alert("Enquiry deleted successfully!");
      if (selectedEnquiryId === id) {
        setSelectedEnquiryId(null);
        setEnquiry({
          enquirerName: "",
          studentName: "",
          status: "Open",
          closureReason: null,
          createdAt: new Date().toISOString().slice(0, 16),
          enquiryQuery: "",
          studentEmail: "",
          inquirerEmail: "",
          studentGender: "",
          enquiryAddress: "",
          studentPhotoUrl: "",
          courseId: "",
          staffId: "",
          followUps: [],
        });
      }
      fetchEnquiries();
    } catch (error) {
      console.error("Error deleting enquiry", error);
      alert("Error deleting enquiry. Check console for details.");
    }
  };

  return (
    <div className="manage-enquiry-container">
      <h2>{selectedEnquiryId ? "Edit Enquiry" : "Create Enquiry"}</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-row">
          <label htmlFor="enquirerName">Enquirer Name:</label>
          <input type="text" id="enquirerName" name="enquirerName" value={enquiry.enquirerName} onChange={handleChange} required />
        </div>
        <div className="form-row">
          <label htmlFor="studentName">Student Name:</label>
          <input type="text" id="studentName" name="studentName" value={enquiry.studentName} onChange={handleChange} required />
        </div>
        <div className="form-row">
          <label htmlFor="studentEmail">Student Email:</label>
          <input type="email" id="studentEmail" name="studentEmail" value={enquiry.studentEmail} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="inquirerEmail">Inquirer Email:</label>
          <input type="email" id="inquirerEmail" name="inquirerEmail" value={enquiry.inquirerEmail} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="studentGender">Student Gender:</label>
          <input type="text" id="studentGender" name="studentGender" value={enquiry.studentGender} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="enquiryAddress">Address:</label>
          <input type="text" id="enquiryAddress" name="enquiryAddress" value={enquiry.enquiryAddress} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="studentPhotoUrl">Photo URL:</label>
          <input type="text" id="studentPhotoUrl" name="studentPhotoUrl" value={enquiry.studentPhotoUrl} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="courseId">Course ID:</label>
          <input type="number" id="courseId" name="courseId" value={enquiry.courseId} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="staffId">Staff ID:</label>
          <input type="number" id="staffId" name="staffId" value={enquiry.staffId} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="enquiryQuery">Enquiry Query:</label>
          <input type="text" id="enquiryQuery" name="enquiryQuery" value={enquiry.enquiryQuery} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="status">Status:</label>
          <select id="status" name="status" value={enquiry.status} onChange={handleChange}>
            <option value={1}>Open</option>
            <option value={2}>Close</option>
            <option value={3}>Registered</option>
          </select>
        </div>
        <div className="form-row">
          <label htmlFor="closureReason">Closure Reason:</label>
          <input type="text" id="closureReason" name="closureReason" value={enquiry.closureReason || ""} onChange={handleChange} />
        </div>
        <div className="form-row">
          <label htmlFor="createdAt">Created At:</label>
          <input type="datetime-local" id="createdAt" name="createdAt" value={enquiry.createdAt} onChange={handleChange} required />
        </div>

        <h3>Follow Ups</h3>
        {enquiry.followUps.map((fu, index) => (
          <div className="follow-up-item" key={fu.followupId}>
            <div className="form-row">
              <label>Follow-up Date:</label>
              <input type="datetime-local" name="followupDate" value={fu.followupDate} onChange={(e) => handleFollowUpChange(index, e)} required />
            </div>
            <div className="form-row">
              <label>Notes:</label>
              <input type="text" name="notes" value={fu.notes} onChange={(e) => handleFollowUpChange(index, e)} placeholder="Notes" required />
            </div>
            <div className="form-row">
              <label>Status:</label>
              <select name="status" value={fu.status} onChange={(e) => handleFollowUpChange(index, e)}>
                <option value="Pending">Pending</option>
                <option value="Completed">Completed</option>
              </select>
            </div>
            <button type="button" onClick={() => deleteFollowUp(index)}>Delete</button>
          </div>
        ))}
        <button type="button" onClick={addFollowUp}>Add Follow-Up</button>
        <button type="submit">{selectedEnquiryId ? "Update" : "Create"}</button>
      </form>

      <h2>All Enquiries</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Enquirer Name</th>
            <th>Student Name</th>
            <th>Status</th>
            <th>Created At</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {enquiries.map((enq) => (
            <tr key={enq.enquiryId}>
              <td>{enq.enquiryId}</td>
              <td>{enq.enquirerName}</td>
              <td>{enq.studentName}</td>
              <td>{enq.status}</td>
              <td>{new Date(enq.createdAt).toLocaleString()}</td>
              <td>
                <button onClick={() => editEnquiry(enq.enquiryId)}>Edit</button>
                <button onClick={() => handleDelete(enq.enquiryId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ManageEnquiry;
