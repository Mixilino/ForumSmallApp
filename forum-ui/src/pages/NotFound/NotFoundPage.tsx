import { Button } from "flowbite-react";
import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../store/AuthContext";
import { useIntl } from "react-intl";
import { messages } from "./messages";
import Layout from "../../components/Layout/Layout";

export const NotFoundPage = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();
  const { formatMessage } = useIntl();

  return (
    <Layout>
      <div className="flex justify-center flex-col items-center flex-grow py-20">
        <h1 className="mb-10 text-4xl text-center font-extrabold tracking-tight leading-none text-gray-900 md:text-5xl lg:text-6xl dark:text-white">
          {formatMessage(messages.notFound)}
        </h1>
        <Button
          onClick={() => {
            navigate(authCtx.isAuthenticated ? "/posts" : "/auth");
          }}
        >
          {formatMessage(messages.goTo, { isAuthenticated: authCtx.isAuthenticated })}
        </Button>
      </div>
    </Layout>
  );
};
