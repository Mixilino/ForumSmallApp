using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class PostCategoriesController : ControllerBase
    {

        private readonly IPostCategoryService postCategoryService;

        public PostCategoriesController(IPostCategoryService pcs)
        {
            this.postCategoryService = pcs;
        }

        [Authorize(Roles = "Admin,Regular")]
        [HttpPost("new")]
        public ActionResult<ServiceResponse<PostCategoryReturnDto>> Create([FromQuery]string name)
        {
            var response = postCategoryService.Create(name);
            if (response.Succes)
            {
                return Created("Succes", response);
            }
            return BadRequest(response);
        }

        [HttpGet("all")]
        public ActionResult<ServiceResponse<PostCategoryReturnDto>> GetAllCategories()
        {
            var response = postCategoryService.GetAll();
            if (response.Succes)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize(Roles = "Admin,Regular")]
        [HttpDelete("delete/{id}")]
        public ActionResult<ServiceResponse<int?>> DeleteCategory(int id)
        {
            var response = postCategoryService.Delete(id);
            if (response.Succes)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize(Roles = "Admin,Regular")]
        [HttpPut("rename")]
        public ActionResult<ServiceResponse<PostCategoryReturnDto>> RenameCategory([FromBody]PostCategoryReturnDto categoryDto)
        {
            var response = postCategoryService.Update(categoryDto);
            if (response.Succes)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
