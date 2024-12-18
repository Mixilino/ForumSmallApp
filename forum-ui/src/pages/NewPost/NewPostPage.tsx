import React, { useEffect, useState } from "react";
import { NewPostForm } from "../../components/posts/PostForm";
import { useGetAllCategories } from "../../hooks/categories/useGetCategories";
import { PostCategory } from "../../model/PostCategories";
import Layout from "../../components/Layout/Layout";

export const NewPostPage = () => {
  const { postCategories } = useGetAllCategories();
  const [categories, setCategories] = useState<PostCategory[]>([]);

  useEffect(() => {
    setCategories(postCategories?.data ?? []);
  }, [postCategories?.data]);

  return (
    <Layout>
      <div className="mt-52 flex justify-center mb-52 ">
        <NewPostForm categories={categories} />
      </div>
    </Layout>
  );
};
