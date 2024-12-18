import { Table } from "flowbite-react";
import React from "react";
import { useIntl } from 'react-intl';
import { UserRoleResponse } from "../../model/UserRolesResponse";
import { SingleUser } from "./SingleUser";
import { messages } from './messages';

interface UsersListProps {
  users: UserRoleResponse[];
}
export const UsersList = ({ users }: UsersListProps) => {
  const { formatMessage } = useIntl();

  return (
    <div className="overflow-hidden">
      <Table hoverable={true}>
        <Table.Head>
          <Table.HeadCell>{formatMessage(messages.userName)}</Table.HeadCell>
          <Table.HeadCell>{formatMessage(messages.role)}</Table.HeadCell>
          <Table.HeadCell align="center">{formatMessage(messages.action)}</Table.HeadCell>
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
