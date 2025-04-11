using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactAPI.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _key;
        private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();

        public AuthController()
        {
            _key = "YourSecretKeyHere_ReplaceWithStrongKey"; // Replace with a secure key
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] LoginRequest login)
        {
            if (login.Username == "admin" && login.Password == "password") // Replace with proper authentication
            {
                var token = GenerateJwtToken(login.Username);
                var refreshToken = Guid.NewGuid().ToString();
                _refreshTokens[refreshToken] = login.Username;

                return Ok(new { Token = token, RefreshToken = refreshToken });
            }
            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            if (_refreshTokens.TryGetValue(refreshRequest.RefreshToken, out var username))
            {
                var newToken = GenerateJwtToken(username);
                var newRefreshToken = Guid.NewGuid().ToString();
                _refreshTokens[newRefreshToken] = username;

                _refreshTokens.Remove(refreshRequest.RefreshToken);

                return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

        public class RefreshRequest
    {
        public string RefreshToken { get; set; }
    }
}
