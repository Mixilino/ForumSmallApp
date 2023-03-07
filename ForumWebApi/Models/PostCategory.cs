using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    [Index(propertyNames: nameof(CategoryName), IsUnique = true)]
    public class PostCategory
    {
        [Key]
        public int PcId { get; set; }
        [Required]
        [StringLength(32)]
        public string CategoryName { get; set; }

        public List<Post> Posts { get; set; }
    }
}
