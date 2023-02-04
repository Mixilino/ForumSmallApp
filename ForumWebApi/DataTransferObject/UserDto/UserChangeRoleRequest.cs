using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.UserDto
{
    public class UserChangeRoleRequest
    {
        public int UserId { get; set; }
        public UserRoles role { get; set; }
    }
}
