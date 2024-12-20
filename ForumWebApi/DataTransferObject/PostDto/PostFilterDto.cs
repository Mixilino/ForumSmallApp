namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostFilterDto
    {
        public string? SearchText { get; set; }
        public List<int>? CategoryIds { get; set; }
        public int? Cursor { get; set; }
        public int PageSize { get; set; } = 10;
    }
} 