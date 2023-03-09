using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    [Index(propertyNames: nameof(UserName), IsUnique = true)]
    public class User
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user. It is a required field
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the hash of the user's password. It is a required field
        /// </summary>
        [Required]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt of the user's password. It is a required field
        /// </summary>
        [Required]
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the Posts list created by the user.
        /// </summary>
        public List<Post> Posts{ get; set; }

        /// <summary>
        /// Gets or sets the Votes list created by the user.
        /// </summary>
        public List<Vote> Votes { get; set; }

        /// <summary>
        /// Gets or sets the Comments list created by the user.
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRoles role { get; set; }

    }

    /// <summary>
    /// Represents the role of a user.
    /// </summary>
    public enum UserRoles
    {
        Regular = 0,
        Admin = 1,
        Banned = 2,
    }
}
