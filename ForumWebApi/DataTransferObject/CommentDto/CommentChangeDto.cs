using ForumWebApi.DataTransferObject.UserDto;

namespace ForumWebApi.DataTransferObject.CommentDto
{
    public class CommentChangeDto
    {
        public CommentChangeDto()
        {

        }
        public CommentChangeDto(int commentId , string commentText)
        {
            this.CommentId = commentId;
            this.CommentText = commentText;
        }
        public int CommentId { get; set; }
        public string CommentText { get; set; }
    }
}
