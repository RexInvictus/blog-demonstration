import React, { useEffect, useState, useRef } from "react";
import SidePanel from "../../Components/SidePanel/SidePanel";
import "./BlogPage.css";
import { FaEye, FaComment } from "react-icons/fa";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
import useWindowDimensions from "../../Components/WindowDimensions";
import { apiUrl } from "../../config";

interface Props {
  searchByTrail: boolean;
}
interface BlogPost {
  id: number;
  titleEN: string;
  subtitleEN: string;
  datePosted: Date;
  viewCount: number;
  coverImageUrl: string;
}

const BlogPage = (props: Props) => {
  const { width } = useWindowDimensions();
  const { id } = useParams();
  const navigate = useNavigate();
  const [blogPosts, setBlogPosts] = useState<BlogPost[]>([]);
  const [pageNumber, setPageNumber] = useState<number>(1);
  const containerRef = useRef<HTMLDivElement>(null);

  const getBlogPosts = async (page: number, orderByMostRecent = true) => {
    try {
      let queryString = `${apiUrl}/api/blogpost?PageNumber=${page}&PageSize=9&OrderByMostRecent=${orderByMostRecent}`;
      if (props.searchByTrail) {
        queryString += `&TrailId=${id}`;
      }
      const response = await axios.get(queryString);
      setBlogPosts(response.data);
    } catch (error) {
    }
  };

  useEffect(() => {
    getBlogPosts(pageNumber);
  }, [pageNumber, props]);

  const navigateToBlogPost = (id: number) => {
    navigate(`/blog/${id}`, { state: { id: id } });
    window.scrollTo({ top: 0, behavior: "instant" as ScrollBehavior });
  };

  return (
    <div className="container">
      <div className="content-container" ref={containerRef}>
        <div className="blog-bulletin-container">
          {blogPosts.map((blogPost, index) => (
            <div
              key={index}
              className="blog-post-preview"
              onClick={() => navigateToBlogPost(blogPost.id)}
            >
              <div style={{ marginLeft: 10 }}>
                <h2>{blogPost.titleEN}</h2>
                <h3>
                  {blogPost.subtitleEN != ""
                    ? blogPost.subtitleEN
                    : blogPost.titleEN}
                </h3>
              </div>
              <img
                src={blogPost.coverImageUrl}
                alt={`Cover for ${blogPost.titleEN}`}
              />
              <div className="info-container-blogpage">
                <div style={{ display: "flex" }}>
                  <p>{new Date(blogPost.datePosted).toLocaleDateString()}</p>
                  <p>
                    <FaEye />
                    {blogPost.viewCount}
                  </p>
                </div>
                {width > 381 &&
                <div>
                  <h3 style={{ color: "blue", marginRight: 10 }}>Read more</h3>
                </div>
              }
              </div>
            </div>
          ))}
        </div>
        <SidePanel comment={false} stretch={false}/>
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

export default BlogPage;
