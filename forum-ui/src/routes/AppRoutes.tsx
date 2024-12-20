import React, { useContext } from "react";
import { Routes, Route, Navigate, BrowserRouter } from "react-router-dom";
import { UserRoles } from "../model/UserRoles";
import { AuthPage } from "../pages/Auth/AuthPage";
import { NewPostPage } from "../pages/NewPost/NewPostPage";
import { NotFoundPage } from "../pages/NotFound/NotFoundPage";
import { AuthContext } from "../store/AuthContext";
import { ManageUsersPage } from "../pages/ManageUsers/ManageUsersPage";
import { EditPostPage } from "../pages/EditPost/EditPostPage";
import { PostsPage } from "../pages/Posts/PostsPage";
import Layout from "../components/Layout/Layout";
import { PostsPageV2 } from "../pages/Posts/PostsPageV2";

export const AppRoutes = () => {
  const authCtx = useContext(AuthContext);
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate replace to="/auth" />} />
        <Route path="/auth" element={<AuthPage />} />
        <Route path="/posts" element={<PostsPageV2 />} />
        {authCtx.isAuthenticated && (
          <>
            <Route path="/posts/old" element={<PostsPage />} />
            <Route path="/new" element={<NewPostPage />} />
            <Route path="/edit-post/:postId" element={<EditPostPage />} />
            {authCtx.role === UserRoles.Admin && (
              <Route path="/admin/users" element={<ManageUsersPage />} />
            )}
          </>
        )}
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </BrowserRouter>
  );
};
