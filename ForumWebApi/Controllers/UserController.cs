using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostService;
using ForumWebApi.services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService us)
        {
            this.userService = us;
        }

        [HttpGet("all")]
        public ActionResult<ServiceResponse<List<UserRoleResponse>>> GetAllCategories()
        {
            var response = userService.GetAll();
            return Ok(response);
        }



        [HttpPatch("change")]
        public ActionResult<ServiceResponse<UserRoleResponse>> ChangeRole([FromBody] UserChangeRoleRequest userDto)
        {
            var response = userService.ChangeRole(userDto);
            if (response.Succes)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
