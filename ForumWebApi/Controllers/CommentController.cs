using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using ForumWebApi.services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{

    /// <summary>
    /// A controller class for managing comments in an API. Client needs to be Authorized to access any of method in this class
    /// </summary>
    [Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        /// <summary>
        /// Creates a new comment. Only Admin and Regular user can access this endpoint.
        /// </summary>
        /// <param name="commentDto">The comment data that is submitted in the request body.</param>
        /// <returns>An ActionResult of type ServiceResponse<CommentResponseDto>, which contains information about the success or failure of the operation.</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpPost("create")]
        public ActionResult<ServiceResponse<CommentResponseDto>> Create([FromBody] CommentCreateDto commentDto)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = commentService.Create(commentDto, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Deletes a comment. Only Admin and Regular user can access this endpoint.
        /// </summary>
        /// <param name="CommentId">The ID of the comment that is to be deleted.</param>
        /// <returns>An ActionResult of type ServiceResponse<CommentResponseDto>, which contains information about the success or failure of the operation.</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpDelete("delete")]
        public ActionResult<ServiceResponse<CommentResponseDto>> Delete([FromQuery] int CommentId)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = commentService.Delete(CommentId, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Changes an existing comment. Only Admin and Regular user can access this endpoint.
        /// </summary>
        /// <param name="commentChangeDto">The comment data that is submitted in the request body.</param>
        /// <returns>An ActionResult of type ServiceResponse<CommentResponseDto>, which contains information about the success or failure of the operation.</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpPatch("edit")]
        public ActionResult<ServiceResponse<CommentResponseDto>> Change(CommentChangeDto commentChangeDto)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = commentService.Change(commentChangeDto, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }
    }
}
