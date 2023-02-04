import { QueryCache, QueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { toast } from "react-toastify";
import { RestResponse } from "../model/RestResponse";

export const queryClient = new QueryClient({
  defaultOptions: {
    mutations: {
      onError: (error) => {
        if (error instanceof AxiosError) {
          const e = error.response?.data as RestResponse;
          const status = error.response?.status;
          toast.error(
            status === 403 ? "You are banned or unauthorized" : e.message
          );
        }
      },
    },
    queries: {
      refetchOnMount: false,
      refetchOnReconnect: false,
      refetchOnWindowFocus: false,
    },
  },
});

export default queryClient;

/*onError: (error: AxiosError) => {
      const e = error.response?.data as RestResponse;
      const status = error.response?.status;
      toast.error(status === 403 ? "You are banned" : e.message);
    }}),*/
