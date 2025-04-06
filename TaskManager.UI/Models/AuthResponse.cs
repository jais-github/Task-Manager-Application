namespace TaskManager.UI.Models
{
    public class AuthResponse
    {
        public bool Success { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
