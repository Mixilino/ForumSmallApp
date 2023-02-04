import { Table } from "flowbite-react";
import React from "react";
import { UserRoleResponse } from "../../model/UserRolesResponse";
import { SingleUser } from "./SingleUser";

interface UsersListProps {
  users: UserRoleResponse[];
}
export const UsersList = ({ users }: UsersListProps) => {
  return (
    <div className="overflow-hidden">
      <Table hoverable={true}>
        <Table.Head>
          <Table.HeadCell>UserName</Table.HeadCell>
          <Table.HeadCell>Role</Table.HeadCell>
          <Table.HeadCell align="center">Action</Table.HeadCell>
        </Table.Head>
        <Table.Body className="divide-y">
          {users.map((u) => (
            <SingleUser user={u} key={u.userId} />
          ))}
        </Table.Body>
      </Table>
    </div>
  );
};
