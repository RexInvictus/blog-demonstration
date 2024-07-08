import React, { useEffect, useState } from "react";
import SidePanel from "../../Components/SidePanel/SidePanel";
import "./GalleryPage.css";
import axios from "axios";
import useWindowDimensions from "../../Components/WindowDimensions";
import { useAuthToken } from "../../Contexts/AuthContext";
import { apiUrl } from "../../config";

interface Props {}
interface GalleryPhoto {
  id: number;
  url: string;
}

const GalleryPage = (props: Props) => {
  const { authToken } = useAuthToken();
  const { width } = useWindowDimensions();
  const [galleryPhotos, setGalleryPhotos] = useState<GalleryPhoto[]>([]);
  const [pageNumber, setPageNumber] = useState<number>(1);
  useEffect(() => {
    const getGalleryPhotos = async (pageNumber: number) => {
      try {
        const response = await axios.get(
          `${apiUrl}/api/gallery?PageNumber=${pageNumber}&PageSize=6`
        );
        setGalleryPhotos(response.data);
      } catch (error) {}
    };

    getGalleryPhotos(pageNumber);
  }, [pageNumber]);

  const handleDelete = async (id: number) => {
    await axios.delete(`${apiUrl}/api/gallery/${id}`, {
      headers: { Authorization: authToken },
    });
  };

  return (
    <div className="container">
      <div className="content-container">
        <div className="gallery-bulletin-container">
          {galleryPhotos.map((galleryPhoto, index) => (
            <div key={index} className="gallery-post-preview">
              {authToken != null && (
                <button onClick={() => handleDelete(galleryPhoto.id)}>
                  delete
                </button>
              )}
              <a href={galleryPhoto.url} target="_blank">
                <img src={galleryPhoto.url} style={{ objectFit: "cover" }} />
              </a>
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

export default GalleryPage;
