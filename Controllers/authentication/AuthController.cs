using Contracts;
using Entities.Dto.auth;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Webbs.Controllers.authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody] AuthDto login)
        {
            if (ModelState.IsValid)
            {
                //search for a predifined user in our store with username and password
                var user = UserStore.Users.FirstOrDefault(
                    u => u.Username == login.Username && u.Password == login.Password
                    );
                if(user == null)
                {
                    return Unauthorized("Invalid user credentials");
                }
                
                var token = GenerateJwtToken(user);
                return Ok(new { access_token = token });
            }
            
            return BadRequest("Invalid payload");
        }

        [HttpGet("users")]
        [Authorize]
        public IActionResult GetUsers()
        {
            string[] users = { "one", "two", "three", "four", "five" };
            _logger.LogInformation($"GetUsers method called");
            return Ok(new { message = users });
        }


        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
