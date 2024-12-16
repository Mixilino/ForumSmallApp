import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { useContext } from "react";
import { AuthContext } from "../../store/AuthContext";
import { RestResponse } from "../../model/RestResponse";
import { useNavigate } from "react-router-dom";

const signInRequest = async (
  vars: signInVariables
): Promise<RestResponse<string>> => {
  const response = await axiosInstanceTs.post("/api/auth/login", {
    username: vars.username,
    password: vars.password,
  });
  const data = response.data as RestResponse<string>;
  return data;
};

interface signInVariables {
  username: string;
  password: string;
}

export const useSignIn = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();

  const { mutateAsync: signInFunc, isLoading: isLoadingSignIn } = useMutation(
    (vars: signInVariables) => signInRequest(vars),
    {
      onSuccess: (data: RestResponse<string>) => {
        authCtx.signIn(data.data);
        navigate("/posts");
      },
    }
  );
  return { signInFunc, isLoadingSignIn };
};
