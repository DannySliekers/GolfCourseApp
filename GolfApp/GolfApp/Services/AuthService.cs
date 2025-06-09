using System.Net.Http.Headers;
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

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginData = new
            {
                username,
                password
            };

            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            { 
                return false;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonDoc = JsonDocument.Parse(responseContent);
            if (jsonDoc.RootElement.TryGetProperty("token", out var tokenElement))
            {
                var token = tokenElement.GetString();

                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.Default.SetAsync("jwt", token);
                    return true;
                }
            }

            return false;
        }
    }
}
