using Microsoft.AspNetCore.Mvc;
using ProjectManager.Api.Models;
using ProjectManager.Domain.Abstractions.Services;

namespace ProjectManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        CancellationToken Token => HttpContext.RequestAborted;

        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            var user = await _userService.FindUserAsync(userId, Token);
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(UserModel user)
        {
            await _userService.CreateUserAsync(user.Name, user.PassHash, Token);
            return Ok();
        }
    }
}
