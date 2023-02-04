import { QueryClientProvider } from "@tanstack/react-query";
import React from "react";
import { ToastContainer } from "react-toastify";
import queryClientInstance from "./api/queryClientInstance";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import "./App.css";
import { AppRoutes } from "./routes/AppRoutes";
import AuthContextProvider from "./store/AuthContext";
import "react-toastify/dist/ReactToastify.css";
import ActivePostContextProvider from "./store/ActivePostContext";

function App() {
  return (
    <>
      <QueryClientProvider client={queryClientInstance}>
        <AuthContextProvider>
          <ActivePostContextProvider>
            <AppRoutes />
          </ActivePostContextProvider>
        </AuthContextProvider>
        <ToastContainer
          position="bottom-center"
          autoClose={2000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
        <ReactQueryDevtools />
      </QueryClientProvider>
    </>
  );
}

export default App;
