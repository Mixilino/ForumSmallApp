import axiosInstanceTs from "../../api/axiosInstance";
import { RestResponse } from "../../model/RestResponse";
import { PostResponse } from "../../model/PostResponse";
import { useMutation } from "@tanstack/react-query";
import { AxiosRequestConfig } from "axios";
import { PostsKey } from "../../constants/queryKeys";
import queryClient from "../../api/queryClientInstance";

const upvotePost = async (vars: upvoteVariables): Promise<PostResponse> => {
  const config: AxiosRequestConfig = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
    params: {
      PostId: vars.postId,
      vote: vars.upvote,
    },
  };
  const { data } = await axiosInstanceTs.get<RestResponse<PostResponse>>(
    "/api/post/vote",
    config
  );
  return data.data;
};

interface upvoteVariables {
  upvote: boolean;
  jwtToken: string;
  postId: number;
}
export function useUpvotePost() {
  const { mutateAsync: upvoteFunc } = useMutation(
    (vars: upvoteVariables) => upvotePost(vars),
    {
      onSuccess: (data: PostResponse) => {
        queryClient.refetchQueries([PostsKey]);
      },
    }
  );
  return { upvoteFunc };
}
