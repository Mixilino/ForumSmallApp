using ForumWebApi.Data.AuthRepo;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{
    /// <summary>
    /// Controller for authentication-related endpoints.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Registers a new user with the given username and password.
        /// </summary>
        /// <param name="userRequest">User registration request data. Consist username and password.</param>
        /// <returns>Service response with the ID of the newly registered user or Error message in case of failure.</returns>
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

        /// <summary>
        /// Logs in the user with the given username and password.
        /// </summary>
        /// <param name="userRequest">User login request data.</param>
        /// <returns>Service response with the JWT token for the authenticated user or error in case of failure.</returns>
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
