using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostCreateDto
    {
        public string PostTitle { get; set; } = string.Empty;
        public string PostText { get; set; } = string.Empty;
        public List<int> PostCategoryIds { get; set; }
    }
}
