using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.Data.UserRoleRepo
{
    public interface IUserRepository
    {
        public List<User> GetAll();

        public User? ChangeRole(UserChangeRoleRequest userDto);

        public User? GetById(int id);
    }
}
