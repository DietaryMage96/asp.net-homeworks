import { useState, useEffect } from "react";
import "./App.css";

const API_URL = "https://localhost:7184/api";

async function api(path, method = "GET", body = null) {
  const res = await fetch(`${API_URL}${path}`, {
    method,
    headers: body ? { "Content-Type": "application/json" } : {},
    body: body ? JSON.stringify(body) : null,
  });

  const text = await res.text();
  let data;
  try { data = JSON.parse(text); } catch { data = text; }

  if (!res.ok) {
    const msg = Array.isArray(data)
      ? data.map((e) => e.errorMessage || e.ErrorMessage).join(" | ")
      : data?.errors
      ? Object.values(data.errors).flat().join(" | ")
      : typeof data === "string" && data ? data : `Error ${res.status}`;
    throw new Error(msg);
  }
  return data;
}

export default function App() {
  const [user, setUser] = useState(() => JSON.parse(localStorage.getItem("user") || "null"));
  const [isReg, setIsReg] = useState(false);
  const [msg, setMsg] = useState({ type: "", text: "" });
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showAdd, setShowAdd] = useState(false);

  const [selectedMovie, setSelectedMovie] = useState(null);
  const [commentText, setCommentText] = useState("");

  useEffect(() => {
    if (!user) return;
    setLoading(true);
    api("/Movie")
      .then(setMovies)
      .catch((e) => setMsg({ type: "error", text: e.message }))
      .finally(() => setLoading(false));
  }, [user]);

  const handleAuth = async (e) => {
    e.preventDefault();
    setMsg({ type: "", text: "" });
    const data = Object.fromEntries(new FormData(e.target));
    if (isReg) data.age = Number(data.age);

    try {
      const res = await api(isReg ? "/User/register" : "/User/login", "POST", data);
      if (isReg) {
        setMsg({ type: "success", text: "Registration successful! Please log in." });
        setIsReg(false);
        e.target.reset();
      } else {
        if (res === false || !res) {
          throw new Error("Invalid login or password");
        }
        const userData = typeof res === "object" ? { login: data.login, ...res } : { login: data.login };
        localStorage.setItem("user", JSON.stringify(userData));
        setUser(userData);
      }
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleAddMovie = async (e) => {
    e.preventDefault();
    setMsg({ type: "", text: "" });
    const data = Object.fromEntries(new FormData(e.target));
    data.releaseYear = Number(data.releaseYear);
    data.rating = Number(data.rating);

    try {
      const created = await api("/Movie", "POST", data);
      setMovies((prev) => [...prev, created]);
      setShowAdd(false);
      e.target.reset();
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleRemove = async (id) => {
    try {
      await api(`/Movie/${id}`, "DELETE");
      setMovies((prev) => prev.filter((m) => m.id !== id));
      if (selectedMovie?.id === id) setSelectedMovie(null);
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleOpenMovie = async (movie) => {
    setMsg({ type: "", text: "" });
    setCommentText("");
    setSelectedMovie(movie);
    try {
      const freshMovie = await api(`/Movie/${movie.id}`);
      setSelectedMovie(freshMovie);
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleAddComment = async (e) => {
    e.preventDefault();
    if (!selectedMovie) return;
    setMsg({ type: "", text: "" });
    const text = commentText.trim();
    if (!text) return;

    try {
      const created = await api("/MovieComment", "POST", {
        movieId: selectedMovie.id,
        authorName: user?.login || user?.name || "User",
        text,
      });

      const updatedComments = [...(selectedMovie.comments || []), created];
      setSelectedMovie((prev) => ({ ...prev, comments: updatedComments }));
      setMovies((prev) =>
        prev.map((m) => (m.id === selectedMovie.id ? { ...m, comments: updatedComments } : m))
      );
      setCommentText("");
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleDeleteComment = async (commentId) => {
    if (!selectedMovie) return;
    try {
      await api(`/MovieComment/${commentId}`, "DELETE");
      const updatedComments = (selectedMovie.comments || []).filter((c) => c.id !== commentId);
      setSelectedMovie((prev) => ({ ...prev, comments: updatedComments }));
      setMovies((prev) =>
        prev.map((m) => (m.id === selectedMovie.id ? { ...m, comments: updatedComments } : m))
      );
    } catch (err) {
      setMsg({ type: "error", text: err.message });
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("user");
    setUser(null);
    setSelectedMovie(null);
  };

  if (!user) {
    return (
      <div className="auth-screen">
        <div className="auth-box">
          <h2 className="auth-title">{isReg ? "REGISTER" : "LOGIN"}</h2>
          {msg.text && <div className={`alert ${msg.type}`}>{msg.text}</div>}

          <form onSubmit={handleAuth} className="auth-form">
            <input name="login" placeholder="Login" required />
            <input name="pass" type="password" placeholder="Password" required />
            {isReg && (
              <>
                <input name="confirmPassword" type="password" placeholder="Confirm Password" required />
                <input name="name" placeholder="Name" required />
                <input name="surname" placeholder="Surname" required />
                <input name="age" type="number" defaultValue={18} placeholder="Age" required />
              </>
            )}
            <div className="auth-buttons">
              <button type="submit" className="btn-primary">{isReg ? "Register" : "Login"}</button>
              <button
                type="button"
                className="btn-secondary"
                onClick={() => { setMsg({ type: "", text: "" }); setIsReg(!isReg); }}
              >
                {isReg ? "Login" : "Register"}
              </button>
            </div>
          </form>
        </div>
      </div>
    );
  }

  return (
    <div className="movies-screen">
      <div className="movies-container">
        <div className="top-bar">
          {selectedMovie ? (
            <button className="btn-add-film" onClick={() => { setMsg({ type: "", text: "" }); setSelectedMovie(null); }}>
              ← Back to Movies
            </button>
          ) : (
            <button className="btn-add-film" onClick={() => setShowAdd(!showAdd)}>
              {showAdd ? "Close" : "Add Film"}
            </button>
          )}

          <div className="user-bar">
            <span><b>{user.login || user.name || "User"}</b></span>
            <button className="btn-logout" onClick={handleLogout}>
              Logout
            </button>
          </div>
        </div>

        {msg.text && <div className={`alert ${msg.type}`}>{msg.text}</div>}

        {selectedMovie ? (
          <div className="movie-details-page">
            <div className="movie-details-hero">
              <div className="details-poster-box">
                <img
                  src={selectedMovie.posterUrl}
                  alt={selectedMovie.title}
                  referrerPolicy="no-referrer"
                  onError={(e) => {
                    e.currentTarget.onerror = null;
                    e.currentTarget.src = "https://placehold.co/400x600/fffcf9/b31a1a?text=No+Poster";
                  }}
                />
              </div>

              <div className="details-info">
                <h1 className="details-title">
                  {selectedMovie.title} <span>({selectedMovie.releaseYear})</span>
                </h1>

                <div className="details-meta-row">
                  <span className="meta-pill">Genre: {selectedMovie.genre}</span>
                  <span className="meta-pill">⭐ {selectedMovie.rating} / 10</span>
                  <span className="meta-pill">Country: {selectedMovie.country}</span>
                </div>

                <div className="details-description">
                  <h3>Description</h3>
                  <p>{selectedMovie.description || "No description provided."}</p>
                </div>

                <div className="details-actions">
                  {selectedMovie.filmUrl && (
                    <a
                      href={selectedMovie.filmUrl}
                      target="_blank"
                      rel="noreferrer"
                      className="btn-primary btn-watch"
                    >
                      Watch Film ↗
                    </a>
                  )}
                  <button className="btn-remove btn-remove-inline" onClick={() => handleRemove(selectedMovie.id)}>
                    Delete Film ✕
                  </button>
                </div>
              </div>
            </div>

            <div className="details-comments-box">
              <h2 className="comments-heading">
                Comments ({(selectedMovie.comments || []).length})
              </h2>

              <form onSubmit={handleAddComment} className="details-comment-form">
                <textarea
                  rows="3"
                  value={commentText}
                  onChange={(e) => setCommentText(e.target.value)}
                  placeholder="Write your comment about this movie..."
                  required
                />
                <button type="submit" className="btn-primary">Post Comment</button>
              </form>

              <div className="details-comments-list">
                {(selectedMovie.comments || []).length === 0 ? (
                  <p className="no-comments">No comments yet. Be the first to share your thoughts!</p>
                ) : (
                  (selectedMovie.comments || []).map((c) => (
                    <div key={c.id} className="details-comment-item">
                      <div className="comment-header">
                        <span className="comment-author">{c.authorName}</span>
                        <div className="comment-meta">
                          <span>{new Date(c.createdAt).toLocaleString("en-US", {
                            year: "numeric",
                            month: "short",
                            day: "numeric",
                            hour: "2-digit",
                            minute: "2-digit",
                          })}</span>
                          <button
                            type="button"
                            className="btn-del-comment"
                            title="Delete comment"
                            onClick={() => handleDeleteComment(c.id)}
                          >
                            ✕
                          </button>
                        </div>
                      </div>
                      <p className="comment-text">{c.text}</p>
                    </div>
                  ))
                )}
              </div>
            </div>
          </div>
        ) : (
          <>
            {showAdd && (
              <form onSubmit={handleAddMovie} className="add-film-form">
                <div className="form-grid">
                  <input name="title" placeholder="Title" required />
                  <input name="genre" placeholder="Genre" required />
                  <input name="releaseYear" type="number" defaultValue={2024} placeholder="Year" required />
                  <input name="rating" type="number" step="0.1" defaultValue={8.5} placeholder="Rating (0-10)" required />
                  <input name="country" placeholder="Country" required />
                  <input name="posterUrl" placeholder="Poster URL" required />
                  <input name="filmUrl" placeholder="Film URL" required />
                  <input name="description" placeholder="Description" />
                </div>
                <button type="submit" className="btn-primary">Save Film</button>
              </form>
            )}

            <div className="movies-grid">
              {loading ? (
                <p className="empty-text">Loading movies from API...</p>
              ) : movies.length === 0 ? (
                <p className="empty-text">No movies found. Click Add Film to create one.</p>
              ) : (
                movies.map((m) => (
                  <div key={m.id} className="film-card">
                    <div
                      className="film-poster-box clickable"
                      onClick={() => handleOpenMovie(m)}
                      title="Open movie details"
                    >
                      <img
                        src={m.posterUrl}
                        alt={m.title}
                        referrerPolicy="no-referrer"
                        onError={(e) => {
                          e.currentTarget.onerror = null;
                          e.currentTarget.src = "https://placehold.co/400x600/fffcf9/b31a1a?text=No+Poster";
                        }}
                      />
                    </div>

                    <div
                      className="film-info-line film-title-line clickable"
                      onClick={() => handleOpenMovie(m)}
                      title={`${m.title} (${m.releaseYear})`}
                    >
                      <b>{m.title}</b> ({m.releaseYear})
                    </div>

                    <div className="film-info-line">
                      {m.genre} • ⭐ {m.rating} • {m.country}
                    </div>

                    <div className="card-actions">
                      <button
                        type="button"
                        className="btn-open-movie"
                        onClick={() => handleOpenMovie(m)}
                      >
                        Details & Comments ({(m.comments || []).length})
                      </button>
                      <button
                        type="button"
                        className="btn-remove"
                        onClick={() => handleRemove(m.id)}
                      >
                        Remove ✕
                      </button>
                    </div>
                  </div>
                ))
              )}
            </div>
          </>
        )}
      </div>
    </div>
  );
}