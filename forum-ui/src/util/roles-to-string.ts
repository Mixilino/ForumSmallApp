import { UserRoles } from "../model/UserRoles";

export const convertRoleStringToEnum = (role: string): UserRoles => {
  if (role === "Admin") return UserRoles.Admin;
  if (role === "Regular") return UserRoles.Regular;
  return UserRoles.Banned;
};

export const convertRolesEnumToString = (role: UserRoles): string => {
  if (role === UserRoles.Admin) return "Admin";
  if (role === UserRoles.Regular) return "Regular";
  return "Banned";
};
