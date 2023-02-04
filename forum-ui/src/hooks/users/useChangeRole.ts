import { useMutation } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { toast } from "react-toastify";
import queryClient from "../../api/queryClientInstance";
import { AxiosError } from "axios";
import { RestResponse } from "../../model/RestResponse";
import { UserRoles } from "../../model/UserRoles";
import { convertRolesEnumToString } from "../../util/roles-to-string";
import { UserRoleResponse } from "../../model/UserRolesResponse";
import { UsersKey } from "../../constants/queryKeys";

const editUserRoleRequest = async (
  vars: changeRoleVariables
): Promise<RestResponse<UserRoleResponse>> => {
  const config = {
    headers: { Authorization: `Bearer ${vars.jwtToken}` },
  };
  return await (
    await axiosInstanceTs.patch<RestResponse<UserRoleResponse>>(
      "/api/users/change",
      {
        userId: vars.userId,
        role: vars.role,
      },
      config
    )
  ).data;
};

interface changeRoleVariables {
  role: UserRoles;
  userId: number;
  jwtToken: string;
}

export const useChangeRole = () => {
  const { mutateAsync: editUserRoleFunc, isLoading } = useMutation(
    (vars: changeRoleVariables) => editUserRoleRequest(vars),
    {
      onSuccess: (response: RestResponse<UserRoleResponse>) => {
        const user = response.data;
        toast.success(
          `${user.userName} role changed to ${convertRolesEnumToString(
            user.role
          )}`
        );
        queryClient.invalidateQueries([UsersKey]);
      },
    }
  );
  return { editUserRoleFunc, isLoading };
};
