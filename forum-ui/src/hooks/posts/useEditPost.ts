import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { toast } from "react-toastify";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import { ActivePostContext } from "../../store/ActivePostContext";

const editPostRequest = async (vars: editPostVariables) => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  await axiosInstanceTs.patch(
    "/api/post/edit",
    {
      postTitle: vars.postTitle,
      postText: vars.postText,
      postId: vars.postId,
      postCategoryIds: vars.postCategoryIds,
    },
    config
  );
};

interface editPostVariables {
  postTitle: string;
  postText: string;
  postCategoryIds: number[];
  postId: number;
  jwtToken: string;
}

export const useEditPost = () => {
  const navigate = useNavigate();
  const activePostCtx = useContext(ActivePostContext);
  const { mutateAsync: editPostFunc, isLoading } = useMutation(
    (vars: editPostVariables) => editPostRequest(vars),
    {
      onSuccess: () => {
        toast.success("Post updated");
        queryClient.refetchQueries([PostsKey]);
        activePostCtx.closeModal();
        navigate("/posts");
      },
    }
  );
  return { editPostFunc, isLoading };
};
