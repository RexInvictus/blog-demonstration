import React, {
  useState,
  useRef,
  useMemo,
  SyntheticEvent,
  ChangeEvent,
  useEffect,
} from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import "./TextEditor.css";
import axios from "axios";
import { useLocation, useNavigate } from "react-router-dom";
import NewTrailForm from "./NewTrailForm";
import { useAuthToken } from "../../Contexts/AuthContext";
import Upload from "../UploadImage/UploadImage";
import { apiUrl } from "../../config";

const formats = [
  "header",
  "font",
  "size",
  "bold",
  "italic",
  "underline",
  "strike",
  "blockquote",
  "list",
  "bullet",
  "indent",
  "link",
  "image",
  "video",
];

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
  comments: [];
  trailId: number;
}

interface Trail {
  id: number;
  name: string;
  coverImageUrl: string;
  km: number;
  days: number;
  status: number;
  DateStarted: Date;
  DateEnded: Date;
}

const TextEditor: React.FC = () => {
  const { authToken } = useAuthToken();
  const { state } = useLocation();
  const handleImageEN = () => {
    const quillEN = quillRefEN.current?.getEditor();
    if (!quillEN) return;
    const range = quillEN.getSelection();
    const value = prompt("What is the image URL");
    if (value) {
      quillEN.insertEmbed(range?.index || 0, "image", value, "user");
    }
  };

  const handleImageLT = () => {
    const quillLT = quillRefLT.current?.getEditor();
    if (!quillLT) return;
    const range = quillLT.getSelection();
    const value = prompt("What is the image URL");
    if (value) {
      quillLT.insertEmbed(range?.index || 0, "image", value, "user");
    }
  };

  const modulesEN = useMemo(
    () => ({
      toolbar: {
        container: [
          [{ header: "1" }, { header: "2" }, { font: [] }],
          [{ size: [] }],
          ["bold", "italic", "underline", "strike", "blockquote"],
          [
            { list: "ordered" },
            { list: "bullet" },
            { indent: "-1" },
            { indent: "+1" },
          ],
          ["link", "image", "video"],
          ["clean"],
        ],
        handlers: {
          image: handleImageEN,
        },
      },
    }),
    []
  );

  const modulesLT = useMemo(
    () => ({
      toolbar: {
        container: [
          [{ header: "1" }, { header: "2" }, { font: [] }],
          [{ size: [] }],
          ["bold", "italic", "underline", "strike", "blockquote"],
          [
            { list: "ordered" },
            { list: "bullet" },
            { indent: "-1" },
            { indent: "+1" },
          ],
          ["link", "image", "video"],
          ["clean"],
        ],
        handlers: {
          image: handleImageLT,
        },
      },
    }),
    []
  );

  const navigate = useNavigate();
  const quillRefEN = useRef<ReactQuill>(null);
  const quillRefLT = useRef<ReactQuill>(null);

  const initialStateEN = state
    ? {
        titleEN: state.titleEN,
        subtitleEN: state.subtitleEN,
        editorHtmlEN: state.contentEN,
        coverPhoto: state.coverImageUrl,
        trailId: state.trailId,
      }
    : {
        titleEN: "",
        subtitleEN: "",
        editorHtmlEN: "",
        coverPhoto: "",
      };

  const initialStateLT = state
    ? {
        titleLT: state.titleLTU,
        subtitleLT: state.subtitleLTU,
        editorHtmlLT: state.contentLTU,
      }
    : {
        titleLT: "",
        subtitleLT: "",
        editorHtmlLT: "",
      };

  const [titleEN, setTitleEN] = useState<string>(initialStateEN.titleEN);
  const [subtitleEN, setSubtitleEN] = useState<string>(
    initialStateEN.subtitleEN
  );
  const [editorHtmlEN, setEditorHtmlEN] = useState<string>(
    initialStateEN.editorHtmlEN
  );

  const [titleLT, setTitleLT] = useState<string>(initialStateLT.titleLT);
  const [subtitleLT, setSubtitleLT] = useState<string>(
    initialStateLT.subtitleLT
  );
  const [editorHtmlLT, setEditorHtmlLT] = useState<string>(
    initialStateLT.editorHtmlLT
  );

  const [coverPhoto, setCoverPhoto] = useState<string>(
    initialStateEN.coverPhoto
  );
  const [selectedTrail, setSelectedTrail] = useState<number>(
    initialStateEN.trailId
  );

  const handleSave = (): void => {
    const post: Post = {
      id: 0,
      viewCount: 0,
      titleEN: titleEN,
      titleLTU: titleLT,
      subtitleEN: subtitleEN,
      subtitleLTU: subtitleLT,
      contentEN: editorHtmlEN,
      contentLTU: editorHtmlLT,
      coverImageUrl: coverPhoto,
      datePosted: new Date(),
      comments: [],
      trailId: selectedTrail,
    };
    navigate(`/blog/0`, { state: { ...post, edit: state ? state.id : false } });
  };

  const handleChangeEN = (html: string): void => {
    setEditorHtmlEN(html);
  };

  const handleChangeLT = (html: string): void => {
    setEditorHtmlLT(html);
  };

  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const handleFileChange = (e: ChangeEvent<HTMLInputElement>): void => {
    const files = e.target.files;
    if (files && files.length > 0) {
      setSelectedFile(files[0]);
    }
  };

  const [loading, setLoading] = useState<boolean>(false);
  const [outputUrl, setOutputUrl] = useState<string>("");

  const handleUpload = async () => {
    if (selectedFile) {
      setLoading(true);
      const formData = new FormData();
      formData.append("Image", selectedFile);
      try {
        const response = await axios.post(
          `${apiUrl}/api/gallery`,
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
              Authorization: authToken,
            },
          }
        );
        setOutputUrl(response.data.url);
      } catch (error) {
      }
      setLoading(false);
    }
  };

  const [availableTrails, setAvailableTrails] = useState<Trail[]>();
  useEffect(() => {
    const getAvailableTrail = async () => {
      try {
        const response = await axios.get(
          `${apiUrl}/api/trail?PageSize=50&OrderByMostRecent=true`
        );

        setAvailableTrails(response.data);
      } catch (error) {
      }
    };

    getAvailableTrail();
  }, []);

  const [trailForDeletion, setTrailForDeletion] = useState<number>(0);
  const handleDeleteTrail = async () => {
    await axios.delete(`${apiUrl}/api/trail/${trailForDeletion}`, {
      headers: {
        Authorization: authToken,
      },
    });
  };

  return (
    <div className="text-editor-container">
      <div className="text-editor">
        <h1>Create/Edit Blogpost</h1>
        <select onChange={(e) => setSelectedTrail(parseInt(e.target.value))}>
          <option value={0}>No trail</option>
          {availableTrails?.map((trail, index) => (
            <option key={index} value={trail.id}>
              {trail.name}
            </option>
          ))}
        </select>
        <div className="language-inputs">
          <div className="english-inputs">
            <h3>English</h3>
            <input
              type="text"
              placeholder="Title (EN)"
              value={titleEN}
              onChange={(e) => setTitleEN(e.target.value)}
            />
            <input
              type="text"
              placeholder="Subtitle (EN)"
              value={subtitleEN}
              onChange={(e) => setSubtitleEN(e.target.value)}
            />
            <ReactQuill
              ref={quillRefEN}
              theme="snow"
              value={editorHtmlEN}
              onChange={handleChangeEN}
              modules={modulesEN}
              formats={formats}
              placeholder="Write something awesome in English..."
            />
          </div>
          <div className="lithuanian-inputs">
            <h3>Lithuanian</h3>
            <input
              type="text"
              placeholder="Title (LT)"
              value={titleLT}
              onChange={(e) => setTitleLT(e.target.value)}
            />
            <input
              type="text"
              placeholder="Subtitle (LT)"
              value={subtitleLT}
              onChange={(e) => setSubtitleLT(e.target.value)}
            />
            <ReactQuill
              ref={quillRefLT}
              theme="snow"
              value={editorHtmlLT}
              onChange={handleChangeLT}
              modules={modulesLT}
              formats={formats}
              placeholder="Write something awesome in Lithuanian..."
            />
          </div>
        </div>
        <input
          type="text"
          placeholder="Cover Photo URL"
          value={coverPhoto}
          onChange={(e) => setCoverPhoto(e.target.value)}
        />
        <br />
        <button onClick={handleSave}>Preview</button>
        <NewTrailForm
          onSuccess={() => {}}
          trails={availableTrails!}
        />
        <h3>Delete a trail</h3>
        <div style={{ display: "flex" }}>
          <select
            onChange={(e) => setTrailForDeletion(parseInt(e.target.value))}
          >
            <option value={0}>Select a trail</option>
            {availableTrails?.map((trail, index) => (
              <option key={index} value={trail.id}>
                {trail.name}
              </option>
            ))}
          </select>
          <button onClick={handleDeleteTrail}>Delete</button>
        </div>
        <Upload setSelectedFile={setSelectedFile} />
        {selectedFile != null && <button onClick={handleUpload}>Upload</button>}
        {loading ? <p>Uploading...</p> : <p>URL is: {outputUrl}</p>}
      </div>
    </div>
  );
};

export default TextEditor;
