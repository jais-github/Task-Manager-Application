using TaskManager.API.Features.Auth;

namespace TaskManager.Helpers;

public interface IJwtHelper
{
    string GenerateToken(User user);
}
