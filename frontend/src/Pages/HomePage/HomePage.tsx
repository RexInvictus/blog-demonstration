import React, { useState, useEffect, useRef } from "react";
import "./HomePage.css";
import { CoverImage1, CoverImage2, CoverImage3, CoverImage4, FaceImage1, FaceImage2 } from "./Urls";
import axios from "axios";
import { useActionData, useNavigate } from "react-router-dom";
import { useAuthToken } from "../../Contexts/AuthContext";
import useWindowDimensions from "../../Components/WindowDimensions";
import ReCAPTCHA from "react-google-recaptcha";
import { apiUrl } from "../../config";

interface Props {}
interface BlogPost {
  id: number;
  viewCount: number;
  titleEN: string;
  subtitleEN: string;
  coverImageUrl: string;
  datePosted: Date;
}

const HomePage = (props: Props) => {
  const { width } = useWindowDimensions();
  const navigate = useNavigate();
  const { setAuthToken } = useAuthToken();
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const [mostRecentPosts, setMostRecentPosts] = useState<BlogPost[]>([]);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [subscribed, setSubscribed] = useState<boolean>(false);
  const images = [CoverImage1, CoverImage2, CoverImage3, CoverImage4];
  const recaptchaRef = useRef<ReCAPTCHA>(null);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImageIndex((old) => (old + 1) % images.length);
    }, 10000);
    return () => clearInterval(interval);
  }, [images]);

  useEffect(() => {
    const faceImages = document.querySelectorAll(".face");
    faceImages.forEach((image) => {
      image.classList.add("fade-in");
    });
    const getMostRecentPosts = async () => {
      try {
        const response = await axios.get(
          `${apiUrl}/api/blogpost?PageNumber=1&PageSize=2&OrderByMostRecent=true`
        );
        setMostRecentPosts(response.data);
      } catch (err) {
      }
    };
    getMostRecentPosts();
  }, []);

  const handleSubscribe = async (t: any) => {
    try {
      setSubscribed(true);

      const response = await axios.post(
        `${apiUrl}/api/subscriber`,
        {
          name: name,
          email: email,
          captchaToken: t,
        }
      );
      if (response.data.accessToken != undefined) {
        setAuthToken(response.data.accessToken);
      } else {
      }
    } catch {
      // do nothing
    }
  };

  const navigateToBlogPost = (id: number) => {
    navigate(`/blog/${id}`, { state: { id: id } });
    window.scrollTo({ top: 0, behavior: "instant" as ScrollBehavior });
  };

  return (
    <div className="container">
      <div
        className="coverDiv"
        style={{
          backgroundImage: `url(${images[currentImageIndex]})`,
        }}
      >
        <div className="infoDiv">
          <div className="card">
            <h3
              style={{
                color: "white",
                fontSize: 30,
                padding: 0,
                marginBottom: 0,
                marginTop: 0,
                textAlign: "center",
                fontFamily: "Garamond",
              }}
            >
              Welcome to Modestas Travels!
            </h3>
            {width > 700 && !subscribed && (
              <p className="info-text">
                Subscribe to my blog to get updates on my latest adventures.
              </p>
            )}
            {subscribed ? (
              <p style={{ color: "white", textAlign: "center", fontSize: 25 }}>
                Subscribed!
              </p>
            ) : (
                <form
                  onSubmit={(e) => {
                    e.preventDefault();
                    recaptchaRef.current?.execute();
                  }}
                >
              <div className="subscribe-form">
                <input
                  type="text"
                  placeholder="Name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required={true}
                />
                <input
                  type="text"
                  placeholder="Email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required={true}
                />
                  <ReCAPTCHA
                    ref={recaptchaRef}
                    size="invisible"
                    sitekey="6LeiyKYpAAAAAKh_7K2vb1BZlNrw8GrsTIP0rV7n"
                    onChange={(t) => {
                      handleSubscribe(t);
                    }}
                  />
                  <button type="submit">Subscribe</button>
              </div>
                </form>
            )}
          </div>
          {width > 1460 && (
            <img src={FaceImage1} className="face" alt="Face 2" />
          )}
        </div>
      </div>
      <div className="divider-div"></div>
      <div className="post-cover-div">
        <section id="about">
          <div className="about-div-home">
            <div className="about-content-home">
              <div className="about-image-div-home">
                <img src={FaceImage2} className="face-image-2" />
              </div>
              <div className="about-text-div-home">
                <h3 className="about-text-h3-home">
                  My name is Modestas Lukauskas, and this is my hiking blog
                </h3>
                <p className="about-text-home">
                  It all started when I found myself at a crossroads in life,
                  feeling a bit bored and in need of a change. It was then that
                  my son suggested I try hiking as a way to break out of my
                  routine and reconnect with nature. Little did I know, that
                  suggestion would spark a deep passion within me.
                </p>
                <p className="about-text-home">
                  From that first step onto the trail, I was hooked. There's
                  something truly magical about being surrounded by the serene
                  beauty of nature, with nothing but the sound of birds chirping
                  and the rustle of leaves underfoot.
                </p>
                <p className="about-text-home">
                  My hiking journey took a significant turn when I embarked on
                  the English coast-to-coast hike. Stretching from the
                  picturesque shores of the Irish Sea to the rugged cliffs of
                  the North Sea, this trail tested both my physical endurance
                  and mental resilience.
                </p>
                <p className="about-text-home">
                  Through this blog, I aim to inspire others to embrace the
                  great outdoors, step out of their comfort zones, and embark on
                  their own epic adventures.
                </p>
                <p className="about-text-home">See you on the trails,</p>
                <p className="about-text-home">Modestas</p>
              </div>
            </div>
          </div>
        </section>
      </div>
      <div className="divider-div"></div>
      <div className="post-cover-div">
        <div style={{ display: "flex", alignItems: "center" }}>
          <h3>Latest Posts</h3>
          <p style={{ marginLeft: 5 }}>or, browse the rest of the blog</p>
          <a style={{ color: "blue", marginLeft: 5 }} href="/blog">here</a>
        </div>
        <div className="post-container">
          {mostRecentPosts.map((mostRecentPost, index) => (
            <div
              className="post"
              onClick={() => {
                navigateToBlogPost(mostRecentPost.id);
              }}
            >
              <div key={index} style={{ marginLeft: 10 }}>
                <h2>{mostRecentPost.titleEN}</h2>
                <h3>
                  {mostRecentPost.subtitleEN != ""
                    ? mostRecentPost.subtitleEN
                    : mostRecentPost.titleEN}
                </h3>
              </div>
              <img
                src={mostRecentPost.coverImageUrl}
                height={200}
                width={404}
              />
              {width > 800 && (
                <div
                  style={{ display: "flex", justifyContent: "space-between" }}
                >
                  <h3 style={{ marginLeft: 10, opacity: 0.2 }}>
                    {new Date(mostRecentPost.datePosted).toLocaleDateString()}
                  </h3>
                  <h3 style={{ color: "blue", marginRight: 10 }}>Read more</h3>
                </div>
              )}
            </div>
          ))}
        </div>
      </div>
      <br />
    </div>
  );
};

export default HomePage;
