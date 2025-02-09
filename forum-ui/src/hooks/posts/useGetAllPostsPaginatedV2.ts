import { useInfiniteQuery } from "@tanstack/react-query";
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

interface FetchParams {
  cursor: number;
  pageSize?: number;
  categoryIds?: number[];
  searchText?: string;
}

const fetchPaginatedPosts = async (
  jwtToken: string,
  { cursor, pageSize = 5, categoryIds, searchText }: FetchParams
): Promise<PaginatedPostsResponse> => {
  const params = new URLSearchParams();
  
  // Add basic params
  params.append('cursor', cursor.toString());
  params.append('pageSize', pageSize.toString());
  
  // Add search text if present
  if (searchText) {
    params.append('searchText', searchText);
  }
  
  // Add category IDs correctly
  if (categoryIds?.length) {
    categoryIds.forEach(id => {
      params.append('CategoryIds', id.toString());
    });
  }

  const config = {
    headers: { 
      Authorization: `Bearer ${jwtToken}`,
    },
    params: params
  };
  
  const { data } = await axiosInstanceTs.get<RestResponse<PaginatedPostsResponse>>(
    "/api/post/paginated-v2",
    config
  );
  return data.data;
};

export function useGetAllPostsPaginatedV2(
  categoryIds?: number[],
  searchText?: string
) {
  const authCtx = useContext(AuthContext);
  
  const {
    data,
    fetchNextPage,
    hasNextPage,
    isFetching,
    isLoading,
  } = useInfiniteQuery(
    [PostsKey, 'paginated-v2', categoryIds, searchText],
    ({ pageParam = 0 }) => fetchPaginatedPosts(authCtx.jwtToken!, { 
      cursor: pageParam,
      categoryIds,
      searchText
    }),
    {
      getNextPageParam: (lastPage) => 
        lastPage.nextCursor !== null ? lastPage.nextCursor : undefined,
      keepPreviousData: true,
      staleTime: 30000,
      cacheTime: 5 * 60 * 1000,
      refetchOnWindowFocus: false,
    }
  );

  const allPosts = data?.pages.flatMap(page => page.posts) ?? [];
  const totalPosts = data?.pages[0]?.totalPosts ?? 0;

  return {
    posts: allPosts,
    totalPosts,
    isLoading,
    isFetching,
    hasNextPage,
    fetchNextPage,
  };
} 