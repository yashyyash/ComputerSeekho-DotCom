// code for Header Component
// This component is responsible for rendering the header of the application
// It includes the logos of VITA and CDAC
// The logos are imported from the public folder
// The component uses CSS for styling
// The component is a functional component

import React from 'react';
import './Header.css';

const Header = () => {
  return (
<header className="header">
  <img src="/Header/LogoFinalPng.png" alt="VITA logo" />
  <img src="/Header/HeaderMain.jpg" alt="Center text" />
</header>

  );
};

export default Header;
