using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GolfApp.Services
{
    public sealed class ImageService : IImageService
    {
        private readonly HttpClient _httpClient; 
        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ImageSource>> GetImagesAsync(int golfCourseId)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/golfcourse/image/{golfCourseId}");

            if (!response.IsSuccessStatusCode)
            {
            }

            var urls = await response.Content.ReadFromJsonAsync<List<string>>();

            if (urls == null || urls.Count == 0) 
            {
                return new List<ImageSource>();
            }



            return urls.Select(url => ImageSource.FromUri(new Uri(TransformUrl(url)))).ToList();
        }

        private string TransformUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return originalUrl;
            }

            const string oldBase = "https://localhost:7129";
            const string newBase = "http://10.0.2.2:5135";

            if (originalUrl.StartsWith(oldBase))
            {
                return newBase + originalUrl.Substring(oldBase.Length);
            }

            return originalUrl;
        }
    }
}
