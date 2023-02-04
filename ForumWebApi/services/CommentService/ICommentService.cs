using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.services.CommentService
{
    public interface ICommentService
    {

        public ServiceResponse<CommentResponseDto> Create(CommentCreateDto postDto, UserResponseDto userDto);
        public ServiceResponse<bool> Delete(int CommentId, UserResponseDto userDto);
        public ServiceResponse<CommentResponseDto> Change(CommentChangeDto commentDto, UserResponseDto userDto);
    }
}
