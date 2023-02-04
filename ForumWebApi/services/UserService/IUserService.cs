using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.services.UserService
{
    public interface IUserService
    {

        public ServiceResponse<List<UserRoleResponse>> GetAll();
        public ServiceResponse<UserRoleResponse> ChangeRole(UserChangeRoleRequest userDto);
    }
}
