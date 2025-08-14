import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Campus.css";

const Campus = () => {
  const [campusData, setCampusData] = useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:7094/api/CampusLife")
      .then((response) => setCampusData(response.data))
      .catch((error) => console.error("Error fetching campus life data:", error));
  }, []);

  return (
    <div className="campus-container">
      <h2 className="campus-title">Campus Life</h2>

      <div className="campus-grid">
        {campusData.map((item) => (
          <div key={item.campusLifeId} className="campus-card">
            {item.photoUrl && (
              <img
                src={item.photoUrl}
                alt="Campus Life"
                className="campus-image"
              />
            )}
            <p className="campus-description">{item.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Campus;
