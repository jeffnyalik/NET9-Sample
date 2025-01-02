using Entities.Dto.auth;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Webbs.Controllers.authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountsController(ILogger<AccountsController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PasswordHash = model.Password
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {   
                    _logger.LogInformation($"User {model.Username} created successfully");
                    return Ok(new {message = "User created successfully" });
                }
                _logger.LogError($"User {model.Username} creation failed");
                return BadRequest(result.Errors);
            }
            _logger.LogError($"Invalid payload");
            return BadRequest(ModelState);
        }

    }
}
