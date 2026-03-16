using CAPGEMINI_CROPDEAL.DTO;
using CAPGEMINI_CROPDEAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CAPGEMINI_CROPDEAL.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _service.Register(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _service.Login(dto);

            return Ok(token);
        }
    }
}