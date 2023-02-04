using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostChangeStateDto
    {
        public int PostId { get; set; }
        public PostStateEnum PostState { get; set; }
    }
}
