import { Button } from "flowbite-react";
import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../store/AuthContext";

export const NotFoundPage = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();

  return (
    <div className="flex justify-center flex-col items-center">
      <h1 className="mb-10 text-4xl text-center mt-10 font-extrabold tracking-tight leading-none text-gray-900 md:text-5xl lg:text-6xl dark:text-white">
        404 Page not found...
      </h1>
      <Button
        onClick={() => {
          navigate(authCtx.isAuthenticated ? "/posts" : "/auth");
        }}
      >
        Go to {authCtx.isAuthenticated ? "All Posts Page" : "Auth Page"}
      </Button>
    </div>
  );
};
