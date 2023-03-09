using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostService;
using ForumWebApi.services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{

    /// <summary>
    /// A controller class for managing post categories in an API. Client needs to be Authorized and have Admin role to access this 
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Constructor for PostController
        /// </summary>
        /// <param name="pcs">UserService for handling User Controller requests</param>
        public UserController(IUserService us)
        {
            this.userService = us;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>ServiceResponse containing all users or error in case of failure.</returns>
        [HttpGet("all")]
        public ActionResult<ServiceResponse<List<UserRoleResponse>>> GetAllUsers()
        {
            var response = userService.GetAll();
            return Ok(response);
        }


        /// <summary>
        /// Changes role of an user.
        /// </summary>
        /// <param name="id">User data containing new role</param>
        /// <returns>ServiceResponse indicating success or failure</returns>
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
