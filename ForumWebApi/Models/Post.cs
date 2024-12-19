using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a post in a forum.
    /// </summary>
    public class Post
    {

        /// <summary>
        /// Gets or sets the unique identifier for the comment. This is primary key in database
        /// </summary>
        [Key]
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the title of the post. This field is required
        /// </summary>
        [Required]
        public string PostTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the text of the post. This field is required
        /// </summary>
        [Required]
        public string PostText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the post.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user object who created the post.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the list of Categories that the post belongs to.
        /// </summary>
        public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();

        /// <summary>
        /// Gets or sets the list of Votes that the post belongs to.
        /// </summary>
        public List<Vote> Votes { get; set; } = new List<Vote>();

        /// <summary>
        /// Gets or sets the list of Comments that the post belongs to.
        /// </summary>
        public List<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        /// Gets or sets the content flag status of the post. 
        /// Normal posts are visible to all, Flagged posts are only visible to admins
        /// </summary>
        public ContentFlagEnum ContentFlag { get; set; } = ContentFlagEnum.Normal;

        /// <summary>
        /// Returns a string representation of the post.
        /// </summary>
        public override string ToString()
        {
            return $"{PostId} {PostText} {DatePosted} {UserId}";
        }
    }
}
