import React, { useEffect, useState } from "react";
import axios from "axios";
import { useAuthToken } from "../../Contexts/AuthContext";
import { apiUrl } from "../../config";

interface NewTrailFormProps {
  onSuccess: () => void;
  trails: {
    id: number;
    name: string;
    coverImageUrl: string;
    km: number;
    days: number;
    status: number;
    DateStarted: Date;
    DateEnded: Date;
  }[];
}

const NewTrailForm: React.FC<NewTrailFormProps> = ({ onSuccess, trails }) => {
  const { authToken } = useAuthToken();
  const [trailName, setTrailName] = useState<string>("");
  const [trailKm, setTrailKm] = useState<number>(0);
  const [trailDays, setTrailDays] = useState<number>(0);
  const [trailStatus, setTrailStatus] = useState<string>("Planned");
  const [trailDateStarted, setTrailDateStarted] = useState<string | null>(null);
  const [trailDateEnded, setTrailDateEnded] = useState<string | null>(null);
  const [coverImageUrl, setCoverImageUrl] = useState<string>("");
  const [editTrail, setEditTrail] = useState<number>(0);

  const createTrail = async () => {
    try {
      const data = {
        Name: trailName,
        Km: trailKm,
        Days: trailDays,
        Status: trailStatus,
        DateStarted: trailDateStarted,
        DateEnded: trailDateEnded,
        CoverImageUrl: coverImageUrl, // Include cover image URL in the request
      };
      if (editTrail == 0) {
        const response = await axios.post(
          `${apiUrl}/api/trail`,
          data,
          {
            headers: {
              Authorization: authToken,
            },
          }
        );
      } else {
        await axios.put(
          `${apiUrl}/api/trail`,
          { ...data, id: editTrail },
          { headers: { Authorization: authToken } }
        );
      }
      onSuccess(); // Notify parent component about success
    } catch (error) {
    }
  };

  return (
    <div className="trail-inputs">
      <h1>Create/Edit New Trail</h1>
      <select onChange={(e) => setEditTrail(parseInt(e.target.value))}>
        <option value={0}>
          Create trail
        </option>
        {trails &&
          trails.map((trail: any, index: number) => (
            <option key={index} value={trail.id}>
              Edit trail: {trail.name}
            </option>
          ))}
      </select>
      <label htmlFor="trailName">Trail Name:</label>
      <input
        id="trailName"
        type="text"
        placeholder="Enter trail name"
        value={trailName}
        onChange={(e) => setTrailName(e.target.value)}
      />
      <label htmlFor="trailKm">Trail Length (Km):</label>
      <input
        id="trailKm"
        type="number"
        placeholder="Enter trail length in kilometers"
        value={trailKm}
        onChange={(e) => setTrailKm(parseInt(e.target.value))}
      />
      <label htmlFor="trailDays">Trail Duration (Days):</label>
      <input
        id="trailDays"
        type="number"
        placeholder="Enter trail duration in days"
        value={trailDays}
        onChange={(e) => setTrailDays(parseInt(e.target.value))}
      />
      <label htmlFor="trailStatus">Trail Status:</label>
      <select
        id="trailStatus"
        value={trailStatus}
        onChange={(e) => setTrailStatus(e.target.value)}
      >
        <option value="Planned">Planned</option>
        <option value="Ongoing">Ongoing</option>
        <option value="Completed">Completed</option>
      </select>
      <label htmlFor="trailDateStarted">Date Started:</label>
      <input
        id="trailDateStarted"
        type="date"
        placeholder="Enter date trail started"
        value={trailDateStarted || ""}
        onChange={(e) => setTrailDateStarted(e.target.value)}
      />
      <label htmlFor="trailDateEnded">Date Ended:</label>
      <input
        id="trailDateEnded"
        type="date"
        placeholder="Enter date trail ended"
        value={trailDateEnded || ""}
        onChange={(e) => setTrailDateEnded(e.target.value)}
      />
      <label htmlFor="coverImageUrl">Cover Image URL:</label>
      <input
        id="coverImageUrl"
        type="text"
        placeholder="Enter cover image URL"
        value={coverImageUrl}
        onChange={(e) => setCoverImageUrl(e.target.value)}
      />
      <button onClick={createTrail}>Create/Edit Trail</button>
    </div>
  );
};

export default NewTrailForm;
