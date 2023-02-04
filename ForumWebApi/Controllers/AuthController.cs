using ForumWebApi.Data.AuthRepo;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto userRequest)
        {
            var response = await _authRepository.Register(new User { UserName = userRequest.Username }, userRequest.Password);
            if (!response.Succes)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserRegisterDto userRequest)
        {
            var response = await _authRepository.Login(userRequest.Username, userRequest.Password);
            if (!response.Succes)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
