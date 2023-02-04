using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.DataTransferObject.PostDto
{
    public class PostResponseDto
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostText { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }

        public PostStateEnum PostState { get; set; }

        public UserResponseDto User { get; set; }

        public List<PostCategoryReturnDto> PostCategories { get; set; }

        public int VotesCount { get; set; }
        public bool Voted { get; set; }
        public bool Upvote { get; set; }

        public List<CommentResponseDto> Comments { get; set; }
    }
}
