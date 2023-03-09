using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.CommentDto
{
    public class CommentResponseDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public UserResponseDto User { get; set; }

        public int PostId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CommentResponseDto other = (CommentResponseDto)obj;
            return CommentId == other.CommentId && 
                CommentText == other.CommentText && 
                DateCreated.Equals(other.DateCreated) && 
                PostId == other.PostId;
        }

        public override int GetHashCode()
        {
            return CommentId.GetHashCode() ^ CommentText.GetHashCode() ^ DateCreated.GetHashCode() ^ PostId.GetHashCode();
        }
    }
}
