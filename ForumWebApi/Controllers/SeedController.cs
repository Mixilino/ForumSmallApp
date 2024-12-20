using ForumWebApi.Data.Seeder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class SeedController : ControllerBase
    {
        private readonly DataSeeder _seeder;

        public SeedController(DataSeeder seeder)
        {
            _seeder = seeder;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                await _seeder.SeedData();
                return Ok(new { message = "Data seeded successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to seed data", error = ex.Message });
            }
        }
    }
} 