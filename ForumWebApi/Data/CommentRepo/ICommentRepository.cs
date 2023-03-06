using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.Data.CommentRepo
{
    public interface ICommentRepository
    {
        public Comment Add(CommentCreateDto commentDto, UserResponseDto userDto);

        public void Delete(int CommentId, UserResponseDto userDto);

        public Comment? Change(CommentChangeDto commentDto, UserResponseDto userDto);
    }
}
