import React, { useContext } from "react";
import { Routes, Route, Navigate, BrowserRouter } from "react-router-dom";
import { UserRoles } from "../model/UserRoles";
import { AuthPage } from "../pages/AuthPage";
import { EditPostPage } from "../pages/EditPostPage";
import { ManageUsersPage } from "../pages/ManageUsersPage";
import { NewPostPage } from "../pages/NewPostPage";
import { NotFoundPage } from "../pages/NotFoundPage";
import { PostsPage } from "../pages/PostsPage";
import { AuthContext } from "../store/AuthContext";

export const AppRoutes = () => {
  const authCtx = useContext(AuthContext);
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate replace to="/auth" />} />
        <Route path="/auth" element={<AuthPage />} />
        {authCtx.isAuthenticated && (
          <>
            <Route path="/posts" element={<PostsPage />} />
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
