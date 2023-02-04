using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    [Index(propertyNames: nameof(UserName), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public List<Post> Posts{ get; set; }

        public List<Vote> Votes { get; set; }
        public List<Comment> Comments { get; set; }

        public UserRoles role { get; set; }

    }

    public enum UserRoles
    {
        Regular = 0,
        Admin = 1,
        Banned = 2,
    }
}
