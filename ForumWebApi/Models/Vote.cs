using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a vote for a post by a user.
    /// </summary>
    public class Vote
    {
        /// <summary>
        /// Gets or sets the ID of the vote.
        /// </summary>
        [Key]
        public int VoteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vote is an upvote.
        /// </summary>
        public bool UpVote { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the vote was made.
        /// </summary>
        public DateTime DateLiked { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who made the vote.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who made the vote.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the ID of the post that was voted on.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the Post that was voted on.
        /// </summary>
        public Post Post { get; set; }
    }

}
