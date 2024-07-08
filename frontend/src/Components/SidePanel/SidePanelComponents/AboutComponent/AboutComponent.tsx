import React, { useState } from "react";
import "./AboutComponent.css";
import AboutImage from "./AboutImage.jpg";
import { FaEye, FaComment, FaExpandAlt, FaMinus } from "react-icons/fa";

interface Props {}

const AboutComponent = (props: Props) => {
  const [showMoreInfo, setShowMoreInfo] = useState<boolean>(false);

  return (
    <div
      className="sidepanel-box"
      onClick={() => {
        setShowMoreInfo((prev) => !prev);
      }}
    >
      <div className="sidepanel-about-image-div">
        <img src={AboutImage} className="sidepanel-about-image" />
      </div>
      <div className="about-me-preview-div">
        <h4>By Modestas</h4>
        <button className="more-info-button">
          {showMoreInfo ? <FaMinus /> : <FaExpandAlt />}
        </button>
      </div>
      <div
        className={
          showMoreInfo ? "show-more-info-div active" : "show-more-info-div"
        }
      >
        <div className="about-div">
          <p className="about-text">My name is Modestas.</p>
          <p className="about-text">
            {" "}
            I am a lifestyle hiker, traveller, mountaineer, and cyclist.
          </p>
          <p className="about-text">
            Subscribe to my blog for updates on my latest adventures.
          </p>
          <p className="about-text">Contact me at</p>
          <p className="about-text">lietuvisanglijoje@gmail.com</p>
        </div>
      </div>
    </div>
  );
};

export default AboutComponent;
