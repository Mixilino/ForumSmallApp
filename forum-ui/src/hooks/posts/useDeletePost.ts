import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { toast } from "react-toastify";
import axiosInstanceTs from "../../api/axiosInstance";
import queryClient from "../../api/queryClientInstance";
import { PostsKey } from "../../constants/queryKeys";
import { RestResponse } from "../../model/RestResponse";

const deletePost = async (vars: deletePostVariables) => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  await axiosInstanceTs.delete(`/api/post/delete/${vars.PostId}`, config);
};

interface deletePostVariables {
  jwtToken: string;
  PostId: number;
}

export const useDeletePost = () => {
  const { mutateAsync: deletePostFunc } = useMutation(
    (vars: deletePostVariables) => deletePost(vars),
    {
      onSuccess: () => {
        toast.success("Post deleted");
        queryClient.refetchQueries([PostsKey]);
      },
    }
  );
  return { deletePostFunc };
};
