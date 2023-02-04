import { Spinner } from "flowbite-react";
import React, { useContext, useEffect, useState } from "react";
import { NavBar } from "../components/navbar/NavBar";
import { useGetAllPosts } from "../hooks/posts/useGetAllPosts";
import { PostsList } from "../components/posts/PostsList";
import { useGetAllCategories } from "../hooks/categories/useGetCategories";
import { PostCategory } from "../model/PostCategories";
import { AuthContext } from "../store/AuthContext";
import { UserRoles } from "../model/UserRoles";

export const PostsPage = () => {
  const { posts: postsData, isLoading } = useGetAllPosts();
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);
  const authCtx = useContext(AuthContext);

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  return (
    <>
      <NavBar />
      {isLoading && (
        <div className="flex justify-center w-screen h-screen items-center">
          <Spinner aria-label="Extra large spinner example" size="xl" />
        </div>
      )}
      {authCtx.role === UserRoles.Banned && (
        <h5 className="text-2xl text-center mt-20 font-bold tracking-tight text-red-600 dark:text-white">
          You are banned!
        </h5>
      )}
      {postsData && !isLoading && (
        <PostsList posts={postsData} categories={categories} />
      )}
    </>
  );
};
