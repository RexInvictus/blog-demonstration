import { createBrowserRouter } from "react-router-dom";
import HomePage from "../Pages/HomePage/HomePage";
import App from "../App";
import TextEditor from "../Components/TextEditor/TextEditor";
import BlogPostPage from "../Pages/BlogPostPage/BlogPostPage";
import BlogPage from "../Pages/BlogPage/BlogPage";
import TrailsPage from "../Pages/TrailsPage/TrailsPage";
import GalleryPage from "../Pages/GalleryPage/GalleryPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "editor", element: <TextEditor /> },
      { path: "/blog/:id", element: <BlogPostPage /> },
      { path: "/blog", element: <BlogPage searchByTrail={false}/> },
      { path: "/blog/trail/:id", element: <BlogPage searchByTrail={true}/> },

      { path: "/trails", element: <TrailsPage /> },
      { path: "/gallery", element: <GalleryPage />}
    ],
  },
]);
