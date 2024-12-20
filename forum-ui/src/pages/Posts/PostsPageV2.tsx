import { Spinner } from "flowbite-react";
import { useContext, useEffect, useState } from "react";
import { useGetAllPostsPaginatedV2 } from "../../hooks/posts/useGetAllPostsPaginatedV2";
import { PostsListV2 } from "../../components/posts/PostsListV2";
import { useGetAllCategories } from "../../hooks/categories/useGetCategories";
import { PostCategory } from "../../model/PostCategories";
import { AuthContext } from "../../store/AuthContext";
import { UserRoles } from "../../model/UserRoles";
import { useIntl } from "react-intl";
import { messages } from "./messages";
import Layout from "../../components/Layout/Layout";
import { PostResponse } from "../../model/PostResponse";

export const PostsPageV2 = () => {
  const [selectedCategories, setSelectedCategories] = useState<PostCategory[]>([]);
  const [searchText, setSearchText] = useState("");
  const { 
    posts, 
    totalPosts, 
    isLoading,
    isFetching,
    hasNextPage,
    fetchNextPage 
  } = useGetAllPostsPaginatedV2(
    selectedCategories.map(c => c.pcId),
    searchText
  );
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);
  const authCtx = useContext(AuthContext);
  const { formatMessage } = useIntl();

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  const loadMore = () => {
    if (!isFetching && hasNextPage) {
      fetchNextPage();
    }
  };

  const handleCategoryChange = (categories: PostCategory[]) => {
    setSelectedCategories(categories);
  };

  const handleSearchChange = (search: string) => {
    setSearchText(search);
  };

  if (isLoading && posts.length === 0) {
    return (
      <Layout>
        <div className="flex justify-center w-screen h-screen items-center">
          <Spinner aria-label="Extra large spinner example" size="xl" />
        </div>
      </Layout>
    );
  }

  return (
    <Layout>
      {authCtx.role === UserRoles.Banned && (
        <h5 className="text-2xl text-center mt-20 font-bold tracking-tight text-red-600 dark:text-white">
          {formatMessage(messages.banned)}
        </h5>
      )}
      <PostsListV2
        posts={posts}
        categories={categories}
        hasMore={!!hasNextPage}
        loadMore={loadMore}
        totalPosts={totalPosts}
        onCategoryChange={handleCategoryChange}
        onSearchChange={handleSearchChange}
        searchText={searchText}
      />
    </Layout>
  );
}; 