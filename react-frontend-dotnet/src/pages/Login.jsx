import React, { useState } from 'react';
import './Login.css';
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const [staffUsername, setUsername] = useState('');
  const [staffPassword, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const response = await fetch('http://localhost:8080/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        // Matching your C# DTO property names
        body: JSON.stringify({
          StaffUsername: staffUsername,
          Password: staffPassword
        })
      });

      if (response.ok) {
        const staffData = await response.json();

        sessionStorage.setItem("isLoggedIn", "true");
        localStorage.setItem("token", staffData.token);
        localStorage.setItem("staffData", JSON.stringify(staffData));

        navigate("/admin");
      } else {
        const errorText = await response.text();
        setError(errorText || '❌ Invalid credentials!');
      }
    } catch (err) {
      console.error("Login error:", err);
      setError('❌ Server error. Try again later.');
    }
  };

  return (
    <div className="login-page">
      <div className="login-glass-card fancy">
        <h2 className="login-title">🚀 Staff Login</h2>
        <p className="login-subtitle">Welcome back! Enter credentials to proceed</p>

        {error && <div className="login-error">{error}</div>}

        <form onSubmit={handleLogin} className="login-form">
          <div className="input-group">
            <i className="fas fa-user"></i>
            <input
              type="text"
              placeholder="Username"
              value={staffUsername}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </div>

          <div className="input-group">
            <i className="fas fa-lock"></i>
            <input
              type="password"
              placeholder="Password"
              value={staffPassword}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <button type="submit" className="glow-button">✨ Login</button>
        </form>

        <p className="login-footer-text">Need help? Contact admin support.</p>
      </div>
    </div>
  );
};

export default Login;
