using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.UserDto
{
    public class UserRoleResponse
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserRoles role { get; set; } 
    }
}
