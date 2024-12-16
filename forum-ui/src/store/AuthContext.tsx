import * as React from "react";
import { createContext, useEffect, useState } from "react";
import jwt_decode from "jwt-decode";
import { convertRoleStringToEnum } from "../util/roles-to-string";

interface AuthContextInterface {
  jwtToken: string | null;
  nameid: number | null;
  username: string | null;
  isAuthenticated: boolean;
  signIn: (token: string, welcome?: boolean) => void;
  signOut: () => void;
  role: number | null;
}

export const AuthContext = createContext<AuthContextInterface>({
  jwtToken: "",
  nameid: 0,
  username: "",
  isAuthenticated: false,
  signIn: async (token: string, welcome?: boolean) => {},
  signOut: async () => {},
  role: null,
});

interface AuthContextProviderProps {
  children?: React.ReactNode;
}

interface JwtSchema {
  nameid: string;
  username: string;
  role: string;
}

function AuthContextProvider({ children }: AuthContextProviderProps) {
  const [authToken, setAuthToken] = useState<string | null>(null);
  const [userName, setUserName] = useState<string | null>(null);
  const [userId, setUserId] = useState<number | null>(null);
  const [role, setRole] = useState<number | null>(null);

  const signIn = async (token: string, welcome?: boolean) => {
    setAuthToken(token);
    const decodedJwt: JwtSchema = jwt_decode(token);
    setUserId(parseInt(decodedJwt.nameid));
    setUserName(decodedJwt.username);
    setRole(convertRoleStringToEnum(decodedJwt.role));

    localStorage.setItem("token", token);
  };

  const signOut = async () => {
    setAuthToken(null);
    localStorage.removeItem("token");
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setAuthToken(token);
      const decodedJwt: JwtSchema = jwt_decode(token);
      setUserId(parseInt(decodedJwt.nameid));
      setUserName(decodedJwt.username);
      setRole(convertRoleStringToEnum(decodedJwt.role));
    }
  }, []);

  const value = {
    jwtToken: authToken,
    isAuthenticated: !!authToken,
    signIn: signIn,
    signOut: signOut,
    nameid: userId,
    username: userName,
    role: role,
  };
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export default AuthContextProvider;
