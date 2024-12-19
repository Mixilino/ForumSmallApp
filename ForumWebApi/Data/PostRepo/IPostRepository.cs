using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;

namespace ForumWebApi.Data.PostRepo
{
    public interface IPostRepository
    {
        public Post Add(PostCreateDto postDto, UserResponseDto userDto);

        public Post? Change(PostChangeDto postDto, UserResponseDto userDto);

        public void Delete(int PostId, UserResponseDto userDto, bool isAdmin);


        public Post? Vote(int PostId, bool UpVote, UserResponseDto userDto);

        public List<Post> GetAll();

        public Post? GetById(int postId);
    }
}
