using Microsoft.AspNetCore.Mvc;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _loginService.AuthenticateAsync(loginRequest);
            return Ok(result);
        }
    }
}