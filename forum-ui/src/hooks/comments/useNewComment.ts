import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { toast } from "react-toastify";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";
import { RestResponse } from "../../model/RestResponse";
import { CommentResponse } from "../../model/CommentResponse";

const newCommentRequest = async (
  vars: newCommentVariables
): Promise<CommentResponse> => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  const resp = await axiosInstanceTs.post<RestResponse<CommentResponse>>(
    "/api/comments/create",
    { CommentText: vars.CommentText, postId: vars.postId },
    config
  );
  return resp.data.data;
};

interface newCommentVariables {
  CommentText: string;
  postId: number;
  jwtToken: string;
}

export const useNewComment = () => {
  const { mutateAsync: newCommentFunc, isLoading } = useMutation(
    (vars: newCommentVariables) => newCommentRequest(vars),
    {
      onSuccess: (c: CommentResponse) => {
        toast.success("Comment created");
        queryClient.refetchQueries([PostsKey]);
      },
    }
  );
  return { newCommentFunc, isLoading };
};
