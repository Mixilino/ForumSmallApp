using ForumWebApi.DataTransferObject.UserDto;

namespace ForumWebApi.DataTransferObject.CommentDto
{
    public class CommentChangeDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
    }
}
