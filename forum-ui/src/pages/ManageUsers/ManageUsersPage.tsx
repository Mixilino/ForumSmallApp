import { Spinner } from "flowbite-react";
import { UsersList } from "../../components/users/UsersList";
import { useGetAllUsers } from "../../hooks/users/useGetAllUsers";
import { NavBar } from "../../components/navbar/NavBar";
import { useIntl } from "react-intl";
import { messages } from "./messages";
import Layout from "../../components/Layout/Layout";

export const ManageUsersPage = () => {
  const { users, isLoading } = useGetAllUsers();
  const { formatMessage } = useIntl();
  return (
    <Layout>
      {isLoading && (
        <div className="flex justify-center w-screen h-screen items-center">
          <Spinner aria-label="Extra large spinner example" size="xl" />
        </div>
      )}
      {!isLoading && (
        <div className="w-full px-4 mb-32 pt-4">
          <h5 className="text-2xl text-center my-20 font-bold tracking-tight text-gray-900 dark:text-white">
            {formatMessage(messages.manageUsers)}
          </h5>
          <div className="max-w-3xl mx-auto border border-gray-200 rounded-lg shadow">
            <div className="overflow-y-auto min-h-[650px] max-h-[calc(100vh-100px)]">
              <UsersList users={users} />
            </div>
          </div>
        </div>
      )}
    </Layout>
  );
};
