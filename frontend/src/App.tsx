import React from "react";
import { Outlet } from "react-router";
import Navbar from "./Components/Navbar/Navbar";
import "./App.css";
import { AuthTokenProvider } from "./Contexts/AuthContext";

function App() {
  return (
    <AuthTokenProvider>
      <Navbar />
      <Outlet />
    </AuthTokenProvider>
  );
}

export default App;
