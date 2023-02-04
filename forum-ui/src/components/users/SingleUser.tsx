import { Table, Tooltip } from "flowbite-react";
import React, { useContext } from "react";
import {
  RiUserAddLine,
  RiUserHeartLine,
  RiUserUnfollowLine,
} from "react-icons/ri";
import { useChangeRole } from "../../hooks/users/useChangeRole";
import { UserRoles } from "../../model/UserRoles";
import { UserRoleResponse } from "../../model/UserRolesResponse";
import { AuthContext } from "../../store/AuthContext";
import { convertRolesEnumToString } from "../../util/roles-to-string";

interface SingleUserProps {
  user: UserRoleResponse;
}
export const SingleUser = ({ user }: SingleUserProps) => {
  const authCtx = useContext(AuthContext);
  const { editUserRoleFunc } = useChangeRole();

  return (
    <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
      <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
        {user.userName}
      </Table.Cell>
      <Table.Cell>{convertRolesEnumToString(user.role)}</Table.Cell>
      <Table.Cell align="center">
        {authCtx.nameid !== user.userId && (
          <div className="flex justify-center gap-1">
            {user.role !== UserRoles.Admin && (
              <Tooltip content="Promote to admin">
                <RiUserHeartLine
                  className="cursor-pointer rounded-lg p-2 hover:bg-gray-100"
                  size={40}
                  onClick={() =>
                    editUserRoleFunc({
                      jwtToken: authCtx.jwtToken!,
                      role: UserRoles.Admin,
                      userId: user.userId,
                    })
                  }
                />
              </Tooltip>
            )}
            {user.role !== UserRoles.Regular && (
              <Tooltip
                content={
                  user.role === UserRoles.Admin
                    ? "Demote to regular user"
                    : "Unban user"
                }
              >
                <RiUserAddLine
                  className="cursor-pointer rounded-lg p-2 hover:bg-gray-100"
                  size={40}
                  onClick={() =>
                    editUserRoleFunc({
                      jwtToken: authCtx.jwtToken!,
                      role: UserRoles.Regular,
                      userId: user.userId,
                    })
                  }
                />
              </Tooltip>
            )}
            {user.role !== UserRoles.Banned && (
              <Tooltip content="Ban user">
                <RiUserUnfollowLine
                  className="cursor-pointer rounded-lg p-2 hover:bg-gray-100"
                  size={40}
                  onClick={() =>
                    editUserRoleFunc({
                      jwtToken: authCtx.jwtToken!,
                      role: UserRoles.Banned,
                      userId: user.userId,
                    })
                  }
                />
              </Tooltip>
            )}
          </div>
        )}
      </Table.Cell>
    </Table.Row>
  );
};
