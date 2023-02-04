using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.CommentDto
{
    public class CommentCreateDto
    {
        public string CommentText { get; set; }
        public int PostId { get; set; }
    }
}
