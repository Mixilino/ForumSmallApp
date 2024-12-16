import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";
import axiosInstanceTs from "../../api/axiosInstance";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";
import { CommentResponse } from "../../model/CommentResponse";

const deleteComment = async (
  vars: deleteCommentVariables
): Promise<CommentResponse> => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
    params: {
      CommentId: vars.comment.commentId,
    },
  };
  await axiosInstanceTs.delete(`/api/comments/delete`, config);
  return vars.comment;
};

interface deleteCommentVariables {
  jwtToken: string;
  comment: CommentResponse;
}

export const useDeleteComment = () => {
  const { mutateAsync: deleteCommentFunc } = useMutation(
    (vars: deleteCommentVariables) => deleteComment(vars),
    {
      onSuccess: (c: CommentResponse) => {
        toast.success("Comment deleted");
        queryClient.invalidateQueries([PostsKey]);
      },
    }
  );
  return { deleteCommentFunc };
};
