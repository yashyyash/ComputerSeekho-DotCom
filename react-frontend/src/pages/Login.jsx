import React, { useState } from 'react';
import './Login.css';
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const [staff_username, setUsername] = useState('');
  const [staff_password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('http://localhost:8080/api/staff/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ staff_username, staff_password })
      });

      if (response.ok) {
        const staffData = await response.json();
        localStorage.setItem("isLoggedIn", "true");
        localStorage.setItem("staffData", JSON.stringify(staffData));
        navigate("/admin");
      } else {
        setError('Invalid credentials');
      }
    } catch (err) {
      console.error("Login error:", err);
      setError('Something went wrong');
    }
  };

  return (
    <div className="login-page">
      <div className="login-wrapper">
        <form className="login-card" onSubmit={handleLogin}>
          <div className="login-header">
            <div className="login-icon">
              {/* You could place a logo here */}
              <span role="img" aria-label="login">🔐</span>
            </div>
            <h2 className="login-title">Staff Login</h2>
            <p className="login-subtext">Please enter your credentials</p>
          </div>

          {error && <div className="error">{error}</div>}

          <div className="login-body">
            <div className="input-group">
              <input
                type="text"
                placeholder="Username"
                value={staff_username}
                onChange={(e) => setUsername(e.target.value)}
                className="login-input"
                required
              />
            </div>
            <div className="input-group">
              <input
                type="password"
                placeholder="Password"
                value={staff_password}
                onChange={(e) => setPassword(e.target.value)}
                className="login-input"
                required
              />
            </div>
          </div>

          <div className="login-footer">
            <button type="submit" className="login-button">Login</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
