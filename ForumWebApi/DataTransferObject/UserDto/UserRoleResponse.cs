using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.UserDto
{
    public class UserRoleResponse
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserRoles role { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            UserRoleResponse other = (UserRoleResponse)obj;
            return UserId == other.UserId && UserName == other.UserName && role==other.role;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode() ^ UserName.GetHashCode() ^ role.GetHashCode();
        }
    }
}
