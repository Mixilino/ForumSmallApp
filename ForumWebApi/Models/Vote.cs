using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public bool UpVote { get; set; }
        public DateTime DateLiked { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
