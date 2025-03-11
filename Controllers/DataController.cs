using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using YourNamespace.Data;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }

        public class ReadingDto
        {
            public int Id { get; set; }
            public long PickUpTime { get; set; } // Unix timestamp
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostDataAsync([FromBody] ReadingDto request)
        {
            if (request == null)
                return BadRequest("Invalid request body.");

            var reading = new Reading
            {
                Id = request.Id,
                PickUpTime = DateTimeOffset.FromUnixTimeSeconds(request.PickUpTime).UtcDateTime
            };

            _context.Readings.Add(reading);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data stored successfully" });
        }
    }
}