using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;

namespace ForumWebApi.services.PostCategoryService
{
    public interface IPostCategoryService
    {
        public ServiceResponse<PostCategoryReturnDto> Create(string name);
        public ServiceResponse<List<PostCategoryReturnDto>> GetAll();
        public ServiceResponse<int?> Delete(int id);
        public ServiceResponse<PostCategoryReturnDto> Update(PostCategoryReturnDto categoryDto); 
    }
}
