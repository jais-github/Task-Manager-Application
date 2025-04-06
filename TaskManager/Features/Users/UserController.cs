using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Features.Auth.DTOs;

namespace TaskManager.Features.Users
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.Username });
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(UpdateUsernameRequest request)
        {
            var success = await _userService.UpdateUsernameAsync(request);
            if (!success) return BadRequest("Failed to update username");

            return Ok("Username updated successfully");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var success = await _userService.ChangePasswordAsync(request);
            if (!success) return BadRequest("Current password is incorrect");

            return Ok("Password changed successfully");
        }
    }
}
