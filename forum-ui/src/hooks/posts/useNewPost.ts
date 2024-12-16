import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { toast } from "react-toastify";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";

const newPostRequest = async (vars: newPostVariables) => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  await axiosInstanceTs.post(
    "/api/post/new",
    {
      postTitle: vars.postTitle,
      postText: vars.postText,
      postCategoryIds: vars.postCategoryIds,
    },
    config
  );
};

interface newPostVariables {
  postTitle: string;
  postText: string;
  postCategoryIds: number[];
  jwtToken: string;
}

export const useNewPost = () => {
  const { mutateAsync: newPostFunc, isLoading } = useMutation(
    (vars: newPostVariables) => newPostRequest(vars),
    {
      onSuccess: () => {
        toast.success("Post created");
        queryClient.refetchQueries([PostsKey]);
      },
    }
  );
  return { newPostFunc, isLoading };
};
