namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostChangeDto
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostText { get; set; } = string.Empty;
        public List<int> PostCategoryIds { get; set; }
    }
}
