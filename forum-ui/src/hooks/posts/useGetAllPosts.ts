import { useQuery } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { useContext } from "react";
import { AuthContext } from "../../store/AuthContext";
import { RestResponse } from "../../model/RestResponse";
import { PostResponse } from "../../model/PostResponse";
import { PostsKey } from "../../constants/queryKeys";

const fetchPosts = async (jwtToken: string): Promise<PostResponse[]> => {
  const config = {
    headers: { Authorization: `Bearer ${jwtToken}` },
  };
  const { data } = await axiosInstanceTs.get<RestResponse<PostResponse[]>>(
    "/api/post/all",
    config
  );
  return data.data;
};

export function useGetAllPosts() {
  const authCtx = useContext(AuthContext);
  const { data: posts = [], isLoading } = useQuery(
    [PostsKey],
    () => fetchPosts(authCtx.jwtToken!),
    {
      keepPreviousData: true,
    }
  );
  return { posts, isLoading };
}
