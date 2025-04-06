using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Features.Auth.DTOs;
using BCrypt.Net;
using TaskManager.API.Data;
using TaskManager.API.Features.Auth;

namespace TaskManager.Features.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int id) ? id : 0;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = GetUserId();
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateUsernameAsync(UpdateUsernameRequest request)
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return false;

            user.Username = request.NewUsername;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                return false;

            var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            //user.PasswordSalt = new byte[0]; // Optional: if using salt separately

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
