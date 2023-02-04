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
    [Authorize]
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService pc)
        {
            this.postService = pc;
        }

        [HttpGet("all")]
        public ActionResult<ServiceResponse<List<PostResponseDto>>> GetAll()
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            var p = postService.GetAll(user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }

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


        [Authorize(Roles = "Admin")]
        [HttpPatch("state")]
        public ActionResult<ServiceResponse<PostResponseDto>> ChangeState(PostChangeStateDto postChangeStateDto)
        {
            var userName = HttpContext.Items["UserName"];
            var userId = HttpContext.Items["UserId"];
            if (userName == null || userId == null)
            {
                return BadRequest(new ServiceResponse<PostResponseDto> { Data = null, Message = "Invalid data", Succes = false });
            }
            UserResponseDto user = new UserResponseDto { UserId = (int)userId, UserName = (string)userName };
            Console.WriteLine(user);
            var p = postService.ChangeState(postChangeStateDto.PostId, postChangeStateDto.PostState, user);
            if (p.Succes)
            {
                return Created("Succes", p);
            }
            return BadRequest(p);
        }
    }
}
