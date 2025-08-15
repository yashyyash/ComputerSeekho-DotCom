import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './Campus.css'; // import CSS

const Campus = () => {
  const [campusData, setCampusData] = useState([]);

  useEffect(() => {
    axios
      .get('http://localhost:8080/api/CampusLife')
      .then((response) => {
        const fixedData = response.data.map((item) => ({
          campusLifeId: item.campusLifeId,
          title: item.title || 'Campus Life',
          description: item.description || '',
          imageUrl: item.photoUrl
            ? item.photoUrl.replace(/^public[\\\/]/, '').replace(/\\/g, '/')
            : null,
        }));
        setCampusData(fixedData);
      })
      .catch((error) => console.error('Error fetching campus life data:', error));
  }, []);

  const handleImageError = (e) => {
    e.currentTarget.src = '/fallback-image.jpg';
    e.currentTarget.classList.remove('opacity-0');
  };

  return (
    <div className="p-6">
      <h2 className="text-3xl font-bold mb-6 text-center">Campus Life</h2>

      <div className="campus-grid">
        {campusData.map((item) => (
          <div key={item.campusLifeId} className="campus-card">
            {item.imageUrl ? (
              <img
  src={`/${item.imageUrl}`}
  alt={item.title}
  className="opacity-0"
  onLoad={(e) => e.currentTarget.classList.remove('opacity-0')}
  onError={handleImageError}
/>
            ) : (
              <div className="w-full h-48 bg-gray-300 flex items-center justify-center">
                <span className="text-gray-600">No Image</span>
              </div>
            )}
            <div className="campus-card-content">
              <h3>{item.title}</h3>
              <p>{item.description}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Campus;
