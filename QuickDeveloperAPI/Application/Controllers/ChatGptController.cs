using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.Services.Models;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ChatGptController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("CreateComplementation")]
        [ProducesResponseType(typeof(ComplementationResponse), 200)]
        [ProducesResponseType(typeof(ComplementationResponse), 400)]
        public async Task<IActionResult> Create([FromBody] ComplementationRequest request)
        {
            var response = await new ChatGptServices(_configuration).CreateComplementation(request);

            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
