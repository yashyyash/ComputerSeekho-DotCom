// src/pages/Campus.jsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Campus = () => {
  const [campusData, setCampusData] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:8080/api/campus-life')
      .then(response => {
        setCampusData(response.data);
      })
      .catch(error => {
        console.error('Error fetching campus life data:', error);
      });
  }, []);

  return (
    <div className="p-4">
      <h2 className="text-2xl font-bold mb-4">Campus Life</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {campusData.map(item => (
          <div key={item.id} className="border rounded-lg shadow-md p-4">
            <img
              src={`http://localhost:5173${item.imageUrl}`}
              alt={item.title}
              className="w-full h-48 object-cover rounded"
            />
            <h3 className="text-lg font-semibold mt-2">{item.title}</h3>
            <p className="text-gray-600">{item.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Campus;
