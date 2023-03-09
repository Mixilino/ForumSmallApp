using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a comment in a forum.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the Comment class without parameters.
        /// </summary>
        public Comment()
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the Comment class with specified parameters.
        /// </summary>
        /// <param name="commentId">The unique identifier for the comment.</param>
        /// <param name="commentText">The text content of the comment.</param>
        /// <param name="dateCreated">The date and time the comment was created.</param>
        /// <param name="userId">The unique identifier for the user who created the comment.</param>
        /// <param name="user">The user object who created the comment.</param>
        /// <param name="postId">The unique identifier for the post the comment belongs to.</param>
        /// <param name="post">The post object the comment belongs to.</param>
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

        /// <summary>
        /// Gets or sets the unique identifier for the comment. This is primary key in database
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// Gets or sets the text content of the comment. CommentText may not be empty
        /// </summary>
        [Required]
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the date and time the comment was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user who created the comment.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user object who created the comment.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the post the comment belongs to.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the post object the comment belongs to.
        /// </summary>
        public Post Post { get; set; }
    }
}
