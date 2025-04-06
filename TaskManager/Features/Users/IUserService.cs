using TaskManager.API.Features.Auth;
using TaskManager.Features.Auth.DTOs;

namespace TaskManager.Features.Users
{
    public interface IUserService
    {
        Task<User?> GetCurrentUserAsync();
        Task<bool> UpdateUsernameAsync(UpdateUsernameRequest request);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
    }
}
