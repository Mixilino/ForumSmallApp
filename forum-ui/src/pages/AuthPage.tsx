import React, { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthForm } from "../components/auth/AuthForm";
import { AuthContext } from "../store/AuthContext";

export const AuthPage = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    if (authCtx.isAuthenticated) {
      navigate("/posts");
    }
  }, [authCtx.isAuthenticated, navigate]);

  return (
    <div className="h-screen w-screen flex justify-center items-center">
      <AuthForm />
    </div>
  );
};
