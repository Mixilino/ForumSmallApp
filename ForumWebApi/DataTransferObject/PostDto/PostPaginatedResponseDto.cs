using ForumWebApi.DataTransferObject.PostDto;

namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostPaginatedResponseDto
    {
        public List<PostResponseDto> Posts { get; set; }
        public int TotalPosts { get; set; }
        public int? NextCursor { get; set; }
    }
} 