import React, { useEffect, useState } from "react";
import axios from "axios";
import "./FacultyList.css";

const FacultyList = () => {
  const [faculties, setFaculties] = useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:7094/api/Faculty") // full API path
      .then((response) => setFaculties(response.data))
      .catch((error) => console.error("Error fetching faculties", error));
  }, []);

  return (
    <div className="faculty-container">
      <h2>Our Faculties</h2>
      <div className="faculty-scroll">
        {faculties.map((faculty) => (
          <div className="faculty-card" key={faculty.facultyId}>
            <img
              src={faculty.photoUrl}
              alt={faculty.facultyName}
              className="faculty-image"
              onError={(e) => {
                e.target.onerror = null; // prevent loop
                e.target.src = "/students/dac_march22/AJAY PATIL Sapiens.jpg"; // fallback image in public folder
              }}
            />
            <h3>{faculty.facultyName}</h3>
            <p>
              <strong>Subject:</strong> {faculty.teachingSubject}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default FacultyList;
