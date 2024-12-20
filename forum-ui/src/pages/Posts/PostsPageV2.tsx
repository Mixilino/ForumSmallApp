import { Spinner } from "flowbite-react";
import { useContext, useEffect, useState } from "react";
import { useGetAllPostsPaginated } from "../../hooks/posts/useGetAllPostsPaginated";
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
  const [cursor, setCursor] = useState(0);
  const [allPosts, setAllPosts] = useState<PostResponse[]>([]);
  const { posts, totalPosts, nextCursor, isLoading } = useGetAllPostsPaginated(cursor);
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);
  const authCtx = useContext(AuthContext);
  const { formatMessage } = useIntl();

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  useEffect(() => {
    if (posts.length > 0) {
      setAllPosts(prev => [...prev, ...posts]);
    }
  }, [posts]);

  const loadMore = () => {
    setCursor(nextCursor);
  };

  const hasMore = allPosts.length < totalPosts;

  if (isLoading && allPosts.length === 0) {
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
        posts={allPosts} 
        categories={categories} 
        hasMore={hasMore}
        loadMore={loadMore}
        totalPosts={totalPosts}
      />
    </Layout>
  );
}; 