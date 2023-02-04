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
    }
}
