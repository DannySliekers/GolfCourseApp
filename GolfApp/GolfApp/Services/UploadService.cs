using GolfApp.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GolfApp.Services
{
    public sealed class UploadService : IUploadService
    {
        private readonly HttpClient _httpClient;

        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            if (fileStream == null || string.IsNullOrEmpty(fileName))
            {
                Debug.WriteLine("Invalid file stream or name.");
            }
            fileStream.Position = 0;

            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            content.Add(streamContent, "image", fileName);

            var response = await _httpClient.PostAsync("/api/upload/image", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Upload failed: {response.StatusCode} - {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<UploadResponse>();

            if (result == null)
            {
                return "";
            }

            return result.ImageUrl;
        }
    }
}
