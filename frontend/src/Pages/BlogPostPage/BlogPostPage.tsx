import React, { useEffect, useState } from "react";
import "./BlogPostPage.css";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { FaEye, FaComment } from "react-icons/fa";
import SidePanel from "../../Components/SidePanel/SidePanel";
import GB from "./GB.png";
import LTU from "./LTU.png";
import axios from "axios";
import { useAuthToken } from "../../Contexts/AuthContext";
import useWindowDimensions from "../../Components/WindowDimensions";
import { apiUrl } from "../../config";

interface Props {}
interface CommentReceive {
  id: number;
  name: string;
  content: string;
}
interface Post {
  id: number;
  viewCount: number;
  titleEN: string;
  titleLTU: string;
  subtitleEN: string;
  subtitleLTU: string;
  coverImageUrl: string;
  contentEN: string;
  contentLTU: string;
  datePosted: Date;
  comments: CommentReceive[];
}

const BlogPostPage: React.FC<Props> = ({}: Props): JSX.Element => {
  const { width } = useWindowDimensions();
  const navigate = useNavigate();
  const [published, setPublished] = useState<boolean>(false);
  const [pageState, setPageState] = useState<Post>();
  const { state } = useLocation();
  const { id } = useParams();
  const [language, setLanguage] = useState<string>("EN");
  const { authToken } = useAuthToken();

  useEffect(() => {
    let isMounted = true;
    const getBlogPost = async (blogId: string) => {
      const response = await axios.get(
        `${apiUrl}/api/blogpost/${blogId}`
      );
      setPageState(response.data);
    };
    if (id === "0") {
      setPageState(state);
    } else {
      getBlogPost(id!);
    }
    const updateViewCount = async () => {
      if (id != "0") {
        await axios.put(
          `${apiUrl}/api/blogpost/update-view-count/${id}`
        );
      }
    };

    const timeout = setTimeout(() => {
      if (isMounted) {
        updateViewCount();
      }
    }, 5000);

    return () => {
      isMounted = false;
    };
  }, [id]);

  const handlePublish = async () => {
    try {
      const postData = {
        titleEN: state.titleEN,
        titleLTU: state.titleLTU,
        subtitleEN: state.subtitleEN,
        subtitleLTU: state.subtitleLTU,
        coverImageUrl: state.coverImageUrl,
        contentEN: state.contentEN,
        contentLTU: state.contentLTU,
        trailId: state.trailId,
      };
      if (!state.edit) {
        const response = await axios.post(
          `${apiUrl}/api/blogpost`,
          postData,
          {
            headers: {
              Authorization: authToken,
            },
          }
        );
      } else {
        const response = await axios.put(
          `${apiUrl}/api/blogpost`,
          { ...postData, id: state.edit },
          {
            headers: {
              Authorization: authToken,
            },
          }
        );
      }
      setPublished(true);
    } catch (error) {
    }
  };

  const handleDelete = async () => {
    try {
      const response = await axios.delete(
        `${apiUrl}/api/blogpost/${pageState?.id}`,
        {
          headers: {
            Authorization: authToken,
          },
        }
      );
    } catch (error) {
    }
  };
  const handleEdit = () => {
    navigate("/editor", {
      state: pageState,
    });
  };

  return (
    <div className="container">
      <div className="cont-container-blogpage">
        {pageState && (
          <div className="blogpost-container">
            <div className="stat-container">
              <div>
                <p>
                  <FaEye /> {pageState.viewCount}
                </p>
              </div>
              <div>
                <p>
                  <FaComment /> {pageState.comments.length}
                </p>
              </div>
            </div>

            <h1 className="title">
              {language === "EN" ? pageState.titleEN : pageState.titleLTU}
            </h1>
            <h2 className="subtitle">
              {language === "EN" ? pageState.subtitleEN : pageState.subtitleLTU}
            </h2>
            <div className="divider"></div>
            <img src={pageState.coverImageUrl} className="cover-photo" alt="" />
            <div className="main-content-container">
              <div className="flag-container">
                <div
                  className={
                    language === "EN"
                      ? "flag-class"
                      : "flag-class flag-inactive"
                  }
                  onClick={() => setLanguage("EN")}
                >
                  <p>English</p>
                  <img src={GB} id="GB-flag" />
                </div>
                <div
                  className={
                    language === "LTU"
                      ? "flag-class"
                      : "flag-class flag-inactive"
                  }
                  onClick={() => setLanguage("LTU")}
                >
                  <p>Lietuviškai</p>
                  <img src={LTU} id="LTU-flag" />
                </div>
              </div>

              <div
                dangerouslySetInnerHTML={{
                  __html:
                    language === "EN"
                      ? pageState.contentEN
                      : pageState.contentLTU,
                }}
              />

              <br />
              <br />
              {pageState.id == 0 &&
                (published ? (
                  <p>Published!</p>
                ) : (
                  <button onClick={handlePublish}>Click to publish</button>
                ))}
              {authToken != null && pageState.id != 0 && (
                <>
                  <button onClick={handleEdit}>Click to edit</button>
                  <button onClick={handleDelete}>Click to delete</button>
                </>
              )}
              {id != "0" && (
                <a
                  href={`https://www.facebook.com/sharer/sharer.php?u=http://www.modestastravels.com/blog/${id}`}
                  target="_blank"
                >
                  Share on Facebook
                </a>
              )}
            </div>
          </div>
        )}
        {pageState && (
          <SidePanel comment={pageState.comments} stretch={width < 838} />
        )}
      </div>
      <br />
    </div>
  );
};

export default BlogPostPage;
