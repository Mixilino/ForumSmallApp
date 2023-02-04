import { Spinner } from "flowbite-react";
import React from "react";
import { NavBar } from "../components/navbar/NavBar";
import { UsersList } from "../components/users/UsersList";
import { useGetAllUsers } from "../hooks/users/useGetAllUsers";

export const ManageUsersPage = () => {
  const { users, isLoading } = useGetAllUsers();
  return (
    <>
      <NavBar />
      {isLoading && (
        <div className="flex justify-center w-screen h-screen items-center">
          <Spinner aria-label="Extra large spinner example" size="xl" />
        </div>
      )}
      {!isLoading && (
        <div className="w-3/4 md:w-144 mx-auto">
          <h5 className="text-2xl text-center my-20 font-bold tracking-tight text-gray-900 dark:text-white">
            Manage Users
          </h5>
          <>
            <UsersList users={users} />
          </>
        </div>
      )}
    </>
  );
};
