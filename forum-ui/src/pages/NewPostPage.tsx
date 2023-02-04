import React, { useEffect, useState } from "react";
import { NavBar } from "../components/navbar/NavBar";
import { NewPostForm } from "../components/posts/PostForm";
import { useGetAllCategories } from "../hooks/categories/useGetCategories";
import { PostCategory } from "../model/PostCategories";

export const NewPostPage = () => {
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  return (
    <>
      <NavBar />
      <div className="mt-52 flex justify-center ">
        <NewPostForm categories={categories} />
      </div>
    </>
  );
};
