import { Spinner } from "flowbite-react";
import { UsersList } from "../../components/users/UsersList";
import { useGetAllUsers } from "../../hooks/users/useGetAllUsers";
import { NavBar } from "../../components/navbar/NavBar";
import { useIntl } from "react-intl";
import { messages } from "./messages";

export const ManageUsersPage = () => {
  const { users, isLoading } = useGetAllUsers();
  const { formatMessage } = useIntl();
  return (
    <>
      {isLoading && (
        <div className="flex justify-center w-screen h-screen items-center">
          <Spinner aria-label="Extra large spinner example" size="xl" />
        </div>
      )}
      {!isLoading && (
        <div className="w-3/4 md:w-144 mx-auto">
          <h5 className="text-2xl text-center my-20 font-bold tracking-tight text-gray-900 dark:text-white">
            {formatMessage(messages.manageUsers)}
          </h5>
          <>
            <UsersList users={users} />
          </>
        </div>
      )}
    </>
  );
};
