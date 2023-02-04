using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostText { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public PostStateEnum PostState { get; set; }

        public List<PostCategory> PostCategories { get; set; }

        public List<Vote> Votes { get; set; }
        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"{PostId} {PostText} {DatePosted} {UserId}";
        }
    }

    public enum PostStateEnum
    {
        In_Verification = 0,
        Verified = 1,
        Not_Verified = 2,
    }
}
