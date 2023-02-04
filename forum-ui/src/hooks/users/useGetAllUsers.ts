import { useQuery } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { useContext } from "react";
import { AuthContext } from "../../store/AuthContext";
import { RestResponse } from "../../model/RestResponse";
import { toast } from "react-toastify";
import { UsersKey } from "../../constants/queryKeys";
import { UserRoleResponse } from "../../model/UserRolesResponse";

const fetchUsers = async (jwtToken: string): Promise<UserRoleResponse[]> => {
  const config = {
    headers: { Authorization: `Bearer ${jwtToken}` },
  };
  const { data } = await axiosInstanceTs.get<RestResponse<UserRoleResponse[]>>(
    "/api/users/all",
    config
  );
  return data.data;
};

export function useGetAllUsers() {
  const authCtx = useContext(AuthContext);
  const { data: users = [], isLoading } = useQuery(
    [UsersKey],
    () => fetchUsers(authCtx.jwtToken!),
    {
      onError: () => {
        toast.error("Error when fetching users");
      },
      keepPreviousData: true,
    }
  );
  return { users, isLoading };
}
