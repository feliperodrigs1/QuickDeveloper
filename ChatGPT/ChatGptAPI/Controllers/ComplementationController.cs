using ChatGptServices.Models;
using ChatGptServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplementationController : ControllerBase
    {
        private readonly ILogger<ComplementationController> _logger;

        public ComplementationController(ILogger<ComplementationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateComplementation")]
        public async Task<IActionResult> Create(RequestModel request)
        {
            var response = await new ComplementationServices().Create(request);

            return Ok(response);
        }
    }
}
