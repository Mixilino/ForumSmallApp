using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ForumWebApi.Resources;

namespace ForumWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        public DebugController(IStringLocalizer<ValidationMessages> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("test-localization")]
        public IActionResult TestLocalization()
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            var titleRequired = _localizer["TitleLength", 3, 50].Value;
            
            return Ok(new 
            { 
                Culture = currentCulture,
                Message = titleRequired
            });
        }
    }
} 