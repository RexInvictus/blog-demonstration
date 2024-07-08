import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import "./Navbar.css";
import logo from "./logo.jpg";
import Komoot from "./Komoot.png";
import Facebook from "./facebook.png";
import Instagram from "./Instagram.png";
import Lichess from "./Lichess.png";
import YouTube from "./YouTube.png";
import { useAuthToken } from "../../Contexts/AuthContext";
import useWindowDimensions from "../WindowDimensions";
import { FaBars } from "react-icons/fa";

interface Props {}

const Navbar = (props: Props) => {
  const location = useLocation();
  const { authToken } = useAuthToken();
  const { width } = useWindowDimensions();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  useEffect(() => {
  }, [width]);

  useEffect(() => {
    const aboutLink = document.getElementById("about");
    if (aboutLink && location.hash === "#about") {
      aboutLink.scrollIntoView({ behavior: "smooth" });
    }
  }, [location]);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const wideDiv = (
    <>
      <Link to="/">
        <div className="imageDiv">
          <img src={logo} alt="" height={80} />
        </div>
      </Link>
      <div className="buttonsDiv">
        <Link to="/" className={"inactive-link"}>
          <h3
            className={
              location.pathname === "/"
                ? "buttonsText activeLink"
                : "buttonsText"
            }
          >
            Home
          </h3>
        </Link>
        <a href="/#about" id="about-link" className={"inactive-link"}>
          <h3 className="buttonsText">About</h3>
        </a>
        <Link to="/blog" className={"inactive-link"}>
          <h3
            className={
              location.pathname === "/blog"
                ? "buttonsText activeLink"
                : "buttonsText"
            }
          >
            Blog
          </h3>
        </Link>
        <Link to="/trails" className={"inactive-link"}>
          <h3
            className={
              location.pathname === "/trails"
                ? "buttonsText activeLink"
                : "buttonsText"
            }
          >
            Trails
          </h3>
        </Link>
        <Link to="/gallery" className={"inactive-link"}>
          <h3
            className={
              location.pathname === "/gallery"
                ? "buttonsText activeLink"
                : "buttonsText"
            }
          >
            Gallery
          </h3>
        </Link>
        {authToken !== null && (
          <Link to="/editor" className="inactive-link">
            <h3
              className={
                location.pathname === "/editor"
                  ? "buttonsText activeLink"
                  : "buttonsText"
              }
            >
              Editor
            </h3>
          </Link>
        )}
      </div>
    </>
  );

  const narrowDiv = (
    <div className="dropdown">
      <button onClick={toggleDropdown} className="dropbtn">
        <FaBars />
      </button>

      {isDropdownOpen && (
        <div className="dropdown-content">
          <Link to="/" className="menu-item">
            Home
          </Link>
          <a href="/#about" id="about-link" className="menu-item">
            About
          </a>
          <Link to="/blog" className="menu-item">
            Blog
          </Link>
          <Link to="/trails" className="menu-item">
            Trails
          </Link>
          <Link to="/gallery" className="menu-item">
            Gallery
          </Link>
          {authToken !== null && (
            <Link to="/editor" className="menu-item">
              Editor
            </Link>
          )}
        </div>
      )}
    </div>
  );

  return (
    <nav className="navContainer">
      <div className="divContainer">
        <div className="imageDiv">
       
          {width > 880 ? wideDiv : narrowDiv}
        </div>
        <div className="buttonsDiv socialMediaDiv">
          <div className="socialMediaButtons">
            <a href="https://www.komoot.com/user/1859599653359" target="_blank">
              <img src={Komoot} alt="" height={30} />
            </a>
          </div>
          <div className="socialMediaButtons">
            <a
              href="https://www.facebook.com/modestas.lukauskas.16/"
              target="_blank"
            >
              <img src={Facebook} alt="" height={28} />
            </a>
          </div>
          <div className="socialMediaButtons">
            <a
              href="https://www.instagram.com/lithuaniaunderground/"
              target="_blank"
            >
              <img src={Instagram} alt="" height={30} />
            </a>
          </div>
          <div className="socialMediaButtons">
            <a
              href="https://www.youtube.com/c/ModestasLukauskas"
              target="_blank"
            >
              <img src={YouTube} alt="" height={30} />
            </a>
          </div>
          <div className="socialMediaButtons">
            <a
              href="https://lichess.org/@/wastingmytimeagain"
              target="_blank"
            >
              <img src={Lichess} alt="" height={30} />
            </a>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
