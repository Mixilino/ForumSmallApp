import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";
import axiosInstanceTs from "../../api/axiosInstance";
import { RestResponse } from "../../model/RestResponse";

const signUpRequest = async (vars: signUpVariables): Promise<RestResponse> => {
  const response = await axiosInstanceTs.post("/api/auth/register", {
    username: vars.username,
    password: vars.password,
  });
  return response.data as RestResponse;
};

interface signUpVariables {
  username: string;
  password: string;
}

export const useSignUp = () => {
  const { mutateAsync: signUpFunc, isLoading: isLoadingSignIn } = useMutation(
    (vars: signUpVariables) => signUpRequest(vars),
    {
      onSuccess: () => {
        toast.success("User succesfully created");
      },
    }
  );
  return { signUpFunc, isLoadingSignIn };
};
