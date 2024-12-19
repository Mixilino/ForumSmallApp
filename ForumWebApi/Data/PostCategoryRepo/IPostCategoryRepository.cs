using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;

namespace ForumWebApi.Data.PostCategoryRepo
{
    public interface IPostCategoryRepository
    {
        public PostCategory Add(string name);
        public Task<List<PostCategory>> GetAll();
        public int Delete(int id);
        public PostCategory? Rename(PostCategoryReturnDto categoryDto);
    }
}
