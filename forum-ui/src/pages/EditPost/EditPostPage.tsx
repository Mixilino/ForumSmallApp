import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { NavBar } from "../../components/navbar/NavBar";
import { useGetAllPosts } from "../../hooks/posts/useGetAllPosts";
import { NewPostForm } from "../../components/posts/PostForm";
import { useGetAllCategories } from "../../hooks/categories/useGetCategories";
import { PostCategory } from "../../model/PostCategories";
import { PostResponse } from "../../model/PostResponse";

export const EditPostPage = () => {
  const params = useParams();
  const [post, setPost] = useState<PostResponse | null>(null);
  const { posts } = useGetAllPosts();
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  useEffect(() => {
    setPost(posts.find((p) => p.postId === parseInt(params.postId!)) ?? null);
  }, [params.postId, posts]);

  return (
    <>
      <div className="mt-52 flex justify-center ">
        <NewPostForm categories={categories} post={post} editMode />
      </div>
    </>
  );
};
