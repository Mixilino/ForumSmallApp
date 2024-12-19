using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostCategoryService;
using ForumWebApi.services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ForumWebApi.Controllers
{
    /// <summary>
    /// A controller class for managing post categories in an API. Client needs to be Authorized to access any of the methods in this class
    /// </summary>
    [Authorize]
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        /// <summary>
        /// Constructor for PostController
        /// </summary>
        /// <param name="pcs">PostService for handling Post Controller requests</param>
        public PostController(IPostService pc)
        {
            this.postService = pc;
        }

        /// <summary>
        /// Gets all posts with pagination
        /// </summary>
        /// <param name="cursor">ID of the first post to fetch from (exclusive). Null means start from beginning.</param>
        /// <param name="pageSize">Number of posts to fetch per page. Defaults to 10.</param>
        /// <returns>ServiceResponse containing paginated posts, total count, and next cursor.</returns>
        [HttpGet("all")]
        public ActionResult<ServiceResponse<List<PostResponseDto>>> GetAll()
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<List<PostResponseDto>> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.GetAll(user);
            if (p.Succes)
            {
                return Ok(p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Gets all posts with pagination
        /// </summary>
        /// <param name="cursor">ID of the first post to fetch from (exclusive). Null means start from beginning.</param>
        /// <param name="pageSize">Number of posts to fetch per page. Defaults to 10.</param>
        /// <returns>ServiceResponse containing paginated posts, total count, and next cursor.</returns>
        [HttpGet("paginated")]
        public ActionResult<ServiceResponse<PostPaginatedResponseDto>> GetAllPaginated([FromQuery] int? cursor = null, [FromQuery] int pageSize = 10)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostPaginatedResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.GetAllPaginated(user, cursor, pageSize);
            if (p.Succes)
            {
                return Ok(p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Creates a new post. User need to be admin or regular.
        /// </summary>
        /// <param name="name">Post data sent in Body of HTTP request</param>
        /// <returns>ServiceResponse containing the created post or error in case of failure.</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpPost("new")]
        public ActionResult<ServiceResponse<PostResponseDto>> Create([FromBody]PostCreateDto post)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if(userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data=null,Message="Invalid data", Succes=false});
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.Create(post, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Changes a post. Only admin and regular users can access this endpoint.
        /// </summary>
        /// <param name="categoryDto">The post to change with updated data.</param>
        /// <returns>ServiceResponse containing the updated post or error in case of failure.</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpPatch("edit")]
        public ActionResult<ServiceResponse<PostResponseDto>> Change(PostChangeDto post)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.Change(post, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Deletes a post by ID. Only admin and regular users can access this endpoint.
        /// </summary>
        /// <param name="id">The ID of the post to delete</param>
        /// <returns>ServiceResponse indicating success or failure</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpDelete("delete/{PostId}")]
        public ActionResult<ServiceResponse<PostResponseDto>> Delete(int PostId)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.Delete(PostId, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Upvote or downvote a post with given ID. Only admin and regular users can access this endpoint.
        /// </summary>
        /// <param name="id">The ID of the category to vote</param>
        /// <returns>ServiceResponse indicating success or failure</returns>
        [Authorize(Roles = "Admin,Regular")]
        [HttpGet("vote")]
        public ActionResult<ServiceResponse<PostResponseDto>> Vote([FromQuery] int PostId, [FromQuery] bool vote)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.Vote(PostId, vote, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

        /// <summary>
        /// Flags a post as inappropriate content. Only admin can access this endpoint.
        /// </summary>
        /// <param name="postId">The ID of the post to flag</param>
        /// <param name="flag">True to flag the post, false to unflag</param>
        /// <returns>ServiceResponse containing the updated post</returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("flag")]
        public ActionResult<ServiceResponse<PostResponseDto>> FlagContent([FromQuery] int postId, [FromQuery] bool flag)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.FlagContent(postId, flag, user);
            if (p.Succes)
            {
                return Ok(p);
            }
            return BadRequest(p);
        }
    }
}
