using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.services.PostService
{
    public interface IPostService
    {
        public ServiceResponse<List<PostResponseDto>> GetAll(UserResponseDto userDto);
        public ServiceResponse<PostPaginatedResponseDto> GetAllPaginated(UserResponseDto userDto, int? cursor = null, int pageSize = 10);
        public ServiceResponse<PostResponseDto> Create(PostCreateDto postDto, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> Change(PostChangeDto postDto, UserResponseDto userDto);
        public ServiceResponse<bool> Delete(int PostId, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> Vote(int PostId, bool vote, UserResponseDto userDto);
        public ServiceResponse<PostResponseDto> FlagContent(int PostId, bool flag, UserResponseDto userDto);
    }
}
