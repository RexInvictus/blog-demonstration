import React, { createContext, useContext, useEffect, useState } from "react";

interface AuthContextType {
  authToken: string | null;
  setAuthToken: React.Dispatch<React.SetStateAction<string | null>>;
}

const AuthContext = createContext<AuthContextType>({
  authToken: null,
  setAuthToken: () => {},
});

export const AuthTokenProvider = ({ children }: any) => {
  const [authToken, setAuthToken] = useState<string | null>(() => {
    // Initialize authToken from local storage if it exists
    return localStorage.getItem("authToken");
  });

  // Update local storage when authToken changes
  useEffect(() => {
    if (authToken) {
      localStorage.setItem("authToken", authToken);
    } else {
      localStorage.removeItem("authToken");
    }
  }, [authToken]);

  return (
    <AuthContext.Provider value={{ authToken, setAuthToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthToken = () => useContext(AuthContext);
