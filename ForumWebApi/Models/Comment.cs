using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
