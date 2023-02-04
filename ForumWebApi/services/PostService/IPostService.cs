using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.services.PostService
{
    public interface IPostService
    {
        public ServiceResponse<List<PostResponseDto>> GetAll(UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> Create(PostCreateDto postDto, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> Change(PostChangeDto postDto, UserResponseDto userDto);
        public ServiceResponse<bool> Delete(int PostId, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> Vote(int PostId, bool vote, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> ChangeState(int PostId, PostStateEnum NewState, UserResponseDto userDto);
    }
}
