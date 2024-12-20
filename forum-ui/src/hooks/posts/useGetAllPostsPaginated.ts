import { useQuery } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { useContext } from "react";
import { AuthContext } from "../../store/AuthContext";
import { RestResponse } from "../../model/RestResponse";
import { PostResponse } from "../../model/PostResponse";
import { PostsKey } from "../../constants/queryKeys";

interface PaginatedPostsResponse {
  posts: PostResponse[];
  totalPosts: number;
  nextCursor: number;
}

const fetchPaginatedPosts = async (
  jwtToken: string,
  cursor: number,
  pageSize: number = 5
): Promise<PaginatedPostsResponse> => {
  const config = {
    headers: { 
      Authorization: `Bearer ${jwtToken}`,
    },
    params: {
      cursor,
      pageSize,
    },
  };
  
  const { data } = await axiosInstanceTs.get<RestResponse<PaginatedPostsResponse>>(
    "/api/post/paginated",
    config
  );
  return data.data;
};

export function useGetAllPostsPaginated(cursor: number) {
  const authCtx = useContext(AuthContext);
  const { data, isLoading } = useQuery(
    [PostsKey, cursor],
    () => fetchPaginatedPosts(authCtx.jwtToken!, cursor),
    {
      keepPreviousData: true,
    }
  );

  return {
    posts: data?.posts ?? [],
    totalPosts: data?.totalPosts ?? 0,
    nextCursor: data?.nextCursor ?? 0,
    isLoading,
  };
} 