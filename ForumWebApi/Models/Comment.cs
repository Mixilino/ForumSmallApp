using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    public class Comment
    {
        public Comment()
        {
            
        }
        public Comment(int commentId, string commentText, DateTime dateCreated, int userId, User user, int postId, Post post)
        {
            CommentId = commentId;
            CommentText = commentText;
            DateCreated = dateCreated;
            UserId = userId;
            User = user;
            PostId = postId;
            Post = post;
        }

        [Key]
        public int CommentId { get; set; }
        [Required]
        public string CommentText { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
