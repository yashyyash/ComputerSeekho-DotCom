import React, { useState, useEffect } from "react";

const AddMarquee = () => {
  const [marqueeText, setMarqueeText] = useState("");
  const [loading, setLoading] = useState(false);
  const [announcements, setAnnouncements] = useState([]);
  const [editId, setEditId] = useState(null);

  // Fetch all announcements
  const fetchAnnouncements = async () => {
    try {
      const res = await fetch("http://localhost:8080/api/announcements");
      if (!res.ok) throw new Error("Failed to fetch announcements");
      const data = await res.json();
      setAnnouncements(data);
    } catch (err) {
      console.error("Error fetching announcements:", err);
    }
  };

  useEffect(() => {
    fetchAnnouncements();
  }, []);

  // Add or Edit announcement
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!marqueeText.trim()) {
      alert("Please enter a marquee text");
      return;
    }

    setLoading(true);
    try {
      let url = "http://localhost:8080/api/announcements";
      let method = "POST";

      if (editId) {
        url = `${url}/${editId}`;
        method = "PUT";
      }

      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          announcementId: editId || 0,
          announcementText: marqueeText,
        }),
      });

      if (!res.ok) throw new Error("Failed to save announcement");

      alert(editId ? "Announcement updated!" : "Announcement added!");
      setMarqueeText("");
      setEditId(null);
      fetchAnnouncements();
    } catch (err) {
      console.error("Error saving announcement:", err);
      alert("Error: " + err.message);
    } finally {
      setLoading(false);
    }
  };

  // Edit handler
  const handleEdit = (id, text) => {
    setEditId(id);
    setMarqueeText(text);
  };

  // Cancel edit
  const handleCancelEdit = () => {
    setEditId(null);
    setMarqueeText("");
  };

  // Delete handler
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this announcement?")) return;

    try {
      const res = await fetch(`http://localhost:8080/api/announcements/${id}`, {
        method: "DELETE",
      });

      if (!res.ok) throw new Error("Failed to delete announcement");

      alert("Announcement deleted!");
      fetchAnnouncements();
    } catch (err) {
      console.error("Error deleting announcement:", err);
      alert("Error: " + err.message);
    }
  };

  return (
    <div className="container mt-4">
      <h2>{editId ? "Edit Announcement" : "Add Marquee Announcement"}</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="marqueeText" className="form-label">
            Announcement Text
          </label>
          <input
            type="text"
            id="marqueeText"
            className="form-control"
            value={marqueeText}
            onChange={(e) => setMarqueeText(e.target.value)}
            placeholder="Enter announcement"
          />
        </div>
        <button type="submit" className="btn btn-primary me-2" disabled={loading}>
          {loading ? "Saving..." : editId ? "Update Announcement" : "Add Announcement"}
        </button>
        {editId && (
          <button type="button" className="btn btn-secondary" onClick={handleCancelEdit}>
            Cancel
          </button>
        )}
      </form>

      <hr />
      <h3>Existing Announcements</h3>
      {announcements.length === 0 ? (
        <p>No announcements found.</p>
      ) : (
        <ul className="list-group">
          {announcements.map((a) => (
            <li
              key={a.announcementId}
              className="list-group-item d-flex justify-content-between align-items-center"
            >
              {a.announcementText}
              <div>
                <button
                  className="btn btn-sm btn-warning me-2"
                  onClick={() => handleEdit(a.announcementId, a.announcementText)}
                >
                  Edit
                </button>
                <button
                  className="btn btn-sm btn-danger"
                  onClick={() => handleDelete(a.announcementId)}
                >
                  Delete
                </button>
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default AddMarquee;
