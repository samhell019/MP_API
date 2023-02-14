using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP_API.Models.InputModels;
using MP_API.Services;

namespace MP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthenticationService _as;
        private ILogger<AuthController> _logger;

        public AuthController(AuthenticationService @as, ILogger<AuthController> logger)
        {
            _as = @as;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginIM credentials)
        {
            var token = _as.Authenticate(new Models.User
            {
                Username = credentials.Username,
                Password = credentials.Password
            });
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterIM entry)
        {
            var user = await _as.Register(entry);
            return Ok(user);
        }
    }
}

