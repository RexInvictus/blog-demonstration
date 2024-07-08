import React, { useReducer, useRef, useState } from "react";
import "./CommentComponent.css";
import { FaPlusCircle, FaMinus } from "react-icons/fa";
import axios from "axios";
import { useParams } from "react-router-dom";
import { useAuthToken } from "../../../../Contexts/AuthContext";
import ReCAPTCHA from "react-google-recaptcha";
import { apiUrl } from "../../../../config";

interface CommentSend {
  blogPostId: string;
  name: string;
  email: string;
  content: string;
  captchaToken?: string;
}

interface Comment {
  id: number;
  name: string;
  email: string;
  content: string;
}

interface Props {
  comments: any;
}

const CommentComponent = (props: Props) => {
  const { authToken } = useAuthToken();
  const { id } = useParams();
  const [name, setName] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [comment, setComment] = useState<string>("");
  const [addComment, setAddComment] = useState<boolean>(false);
  const [comments, setComments] = useState<Comment[]>(props.comments);
  const recaptchaRef = useRef<ReCAPTCHA>(null);

  const handlePostComment = async (t: any) => {
    try {
      const newComment: CommentSend = {
        blogPostId: id!,
        name: name,
        email: email,
        content: comment,
        captchaToken: t,
      };
      setName("");
      setEmail("");
      setComment("");
      setComments((prev) => [
        ...prev,
        { ...newComment, id: comments.length + 1 },
      ]);

      await axios.post(`${apiUrl}/api/comment`, newComment, {
        headers: {
          "Content-Type": "application/json-patch+json",
        },
      });
    } catch (err) {
      alert(
        "There was an error posting your comment. Check your internet connection."
      );
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await axios.delete(`${apiUrl}/api/comment/${id}`, {
        headers: { Authorization: authToken },
      });
    } catch {}
  };

  return (
    <div className="sidepanel-box">
      <div className="title-comments">
        <h3>Comments</h3>
      </div>
      <div className="comment-item-div">
        {comments.map((c, index) => (
          <div key={index} className="comment-item-box">
            <h4>{c.name}</h4>
            <p>{c.content}</p>
            {authToken && (
              <button
                style={{ marginLeft: 10, marginBottom: 10 }}
                onClick={() => handleDelete(c.id)}
              >
                Delete
              </button>
            )}
          </div>
        ))}
      </div>
      <div className="create-comment-div">
        <div
          className="create-comment-title-div-comment"
          onClick={() => {
            setAddComment((prev) => !prev);
          }}
        >
          <h4>Comment</h4>
          <h4>{addComment ? <FaMinus /> : <FaPlusCircle />}</h4>
        </div>
        <div className={addComment ? "active" : "show-more-info-div"}>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              recaptchaRef.current?.reset();
              recaptchaRef.current?.execute();
            }}
          >
            <div className="name-email-div-comment">
              <input
                type="text"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                maxLength={20}
                required={true}
              />
              <input
                type="text"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required={true}
              />
            </div>
            <textarea
              placeholder="Write a comment..."
              value={comment}
              onChange={(e) => setComment(e.target.value)}
            />

            <ReCAPTCHA
              ref={recaptchaRef}
              size="invisible"
              sitekey="6LeiyKYpAAAAAKh_7K2vb1BZlNrw8GrsTIP0rV7n"
              onChange={(t) => {
                handlePostComment(t);
              }}
            />
            <button type="submit">Post</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CommentComponent;
