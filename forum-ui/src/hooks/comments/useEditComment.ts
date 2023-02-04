import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { toast } from "react-toastify";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";
import { AxiosError } from "axios";
import { RestResponse } from "../../model/RestResponse";

const editCommentRequest = async (vars: editCommentVariables) => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  await axiosInstanceTs.patch(
    "/api/comments/edit",
    {
      commentId: vars.commentId,
      commentText: vars.commentText,
    },
    config
  );
};

interface editCommentVariables {
  commentId: number;
  commentText: string;
  jwtToken: string;
}

export const useEditComment = () => {
  const { mutateAsync: editCommentFunc, isLoading } = useMutation(
    (vars: editCommentVariables) => editCommentRequest(vars),
    {
      onSuccess: () => {
        toast.success("Comment updated");
        queryClient.refetchQueries([PostsKey]);
      },
    }
  );
  return { editCommentFunc, isLoading };
};
