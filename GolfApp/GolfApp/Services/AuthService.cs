using System.Text;
using System.Text.Json;

namespace GolfApp.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(string email, string username, string password)
        {
            var registrationData = new
            {
                email,
                username,
                password
            };

            var json = JsonSerializer.Serialize(registrationData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/auth/register", content);
            return response.IsSuccessStatusCode;
        }
    }
}
