using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{
    /// <summary>
    /// A controller class for managing post categories in an API. Client needs to be Authorized to access any of method in this class
    /// </summary>
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class PostCategoriesController : ControllerBase
    {

        private readonly IPostCategoryService postCategoryService;

        /// <summary>
        /// Constructor for PostCategoriesController
        /// </summary>
        /// <param name="pcs">Service for handling post categories</param>
        public PostCategoriesController(IPostCategoryService pcs)
        {
            this.postCategoryService = pcs;
        }

        /// <summary>
        /// Creates a new post category. User need to be admin or regular.
        /// </summary>
        /// <param name="name">The name of the new category</param>
        /// <returns>ServiceResponse containing the created category</returns>
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

        /// <summary>
        /// Gets all post categories
        /// </summary>
        /// <returns>ServiceResponse containing all categories</returns>
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

        /// <summary>
        /// Deletes a post category by ID. Only admin and regular users can access this endpoint.
        /// </summary>
        /// <param name="id">The ID of the category to delete</param>
        /// <returns>ServiceResponse indicating success or failure</returns>
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

        /// <summary>
        /// Renames a post category. Only admin and regular users can access this endpoint.
        /// </summary>
        /// <param name="categoryDto">The post category to rename with updated data</param>
        /// <returns>ServiceResponse containing the updated category or error in case of failure.</returns>
        /// [Authorize(Roles = "Admin,Regular")]
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
