using Application.Models;
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
        public async Task<IActionResult> Create([FromBody] ComplementationRequest request)
        {
            var response = new CustomReturn<ComplementationResponse>();
            response.Data = await new ChatGptServices(_configuration).CreateComplementation(request);

            return Ok(response);
        }
    }
}
