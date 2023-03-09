using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a category that a post can be assigned to.
    /// </summary>
    [Index(propertyNames: nameof(CategoryName), IsUnique = true)]
    public class PostCategory
    {
        /// <summary>
        /// Gets or sets the unique identifier of the category.
        /// </summary>
        [Key]
        public int PcId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category. Required and limited to 32 characters.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the list of posts that are assigned to this category.
        /// </summary>
        public List<Post> Posts { get; set; }
    }
}
