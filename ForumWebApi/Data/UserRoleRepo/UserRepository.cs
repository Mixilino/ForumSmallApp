using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebApi.Data.UserRoleRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User? ChangeRole(UserChangeRoleRequest userDto)
        {
            User user = _context.Users.SingleOrDefault(u => u.UserId == userDto.UserId);
            if (user == null)
            {
                return null;
            }
            user.role = userDto.role;
            return user;
        }

        public List<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User? GetById(int id) {
            return _context.Users.SingleOrDefault(u => u.UserId == id);
        }
    }
}
