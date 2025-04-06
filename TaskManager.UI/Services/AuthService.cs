using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskManager.UI.Models;

namespace TaskManager.UI.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResponse?> RegisterAsync(AuthRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", request);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<AuthResponse?> LoginAsync(AuthRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
    }
}
