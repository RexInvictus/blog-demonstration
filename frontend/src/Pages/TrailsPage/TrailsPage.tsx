import React, { useEffect, useState } from "react";
import SidePanel from "../../Components/SidePanel/SidePanel";
import "./TrailsPage.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import useWindowDimensions from "../../Components/WindowDimensions";
import { apiUrl } from "../../config";

interface Props {}
interface Trail {
  id: number;
  name: string;
  coverImageUrl: string;
  km: number;
  days: number;
  status: number;
  dateStarted: Date;
  dateEnded: Date;
}

const TrailsPage = (props: Props) => {
  const { width } = useWindowDimensions();
  const [trails, setTrails] = useState<Trail[]>([]);
  const [pageNumber, setPageNumber] = useState<number>(1);
  const navigate = useNavigate();
  useEffect(() => {
    const getTrails = async () => {
      try {
      const response = await axios.get(
        `${apiUrl}/api/trail?PageNumber=${pageNumber}&PageSize=4&OrderByMostRecent=true`
      );
      setTrails(response.data);
      }
      catch{}
    };
    getTrails();
  }, [pageNumber]);

  const handleTrailClick = (trailId: number) => {
    navigate(`/blog/trail/${trailId}`);
  };
  return (
    <div className="container">
      {width < 838 && <h3>Trails</h3>}
      <div className="content-container">
        <div className="trails-bulletin-container">
          {trails.map((trail, index) => (
            <div
              key={index}
              className="trails-post-preview"
              style={{
                backgroundImage: `url(${trail.coverImageUrl})`,
                backgroundSize: "cover",
                backgroundPosition: "center",
              }}
              onClick={() => handleTrailClick(trail.id)}
            >
              <div className="trails-post-preview-overlay">
                <div className={`trail-status-div status-${trail.status}`}>
                  <h4>
                    {trail.status == 0
                      ? "Planned"
                      : trail.status == 1
                      ? "Ongoing"
                      : "Complete"}
                  </h4>
                </div>
                <div style={{ width: "80%", textAlign: "center" }}>
                  <h1 style={{ margin: 0 }}>{trail.name}</h1>
                  <h2 style={{ margin: 0 }}>{trail.km} km</h2>
                  <h2 style={{ margin: 0 }}>{trail.days} days</h2>
                  {trail.dateStarted != null && trail.dateEnded != null && (
                    <h2 style={{ margin: 0, fontSize: "small", marginTop: 5 }}>
                      {new Date(trail.dateStarted).toLocaleDateString()} -{" "}
                      {new Date(trail.dateEnded).toLocaleDateString()}
                    </h2>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>

        {width > 838 && <SidePanel comment={false} stretch={false} />}
      </div>
      <div className="next-page-div">
        <button
          onClick={() => {
            setPageNumber((prev) => prev - 1);
          }}
          className={pageNumber === 1 ? "invisible" : ""}
        >
          Previous Page
        </button>
        <button
          onClick={() => {
            setPageNumber((prev) => prev + 1);
          }}
        >
          Next Page
        </button>
      </div>
    </div>
  );
};

export default TrailsPage;
