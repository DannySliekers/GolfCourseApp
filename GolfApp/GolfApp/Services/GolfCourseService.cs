using GolfApp.Helpers;
using GolfApp.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GolfApp.Services
{
    public sealed class GolfCourseService : IGolfCourseService
    {
        private readonly HttpClient _httpClient;

        public GolfCourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> AddGolfCourseAsync(GolfCourse course)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(course);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/golfcourse", content);

            var uploadedCourse = await response.Content.ReadFromJsonAsync<GolfCourse>();

            if (uploadedCourse == null)
            {
                return 0;
            }

            return uploadedCourse.Id;
        }

        public async Task<bool> AddImageToGolfCourseAsync(int id, string imageUrl)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                golfCourseId = id,
                url = imageUrl
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/golfcourse/image", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGolfCourseAsync(int id)
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/golfcourse?id={id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<GolfCourse>> GetAllGolfCoursesAsync()
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/golfcourse");

            if (!response.IsSuccessStatusCode)
            {
                return new List<GolfCourse>();
            }

            var courses = await response.Content.ReadFromJsonAsync<List<GolfCourse>>();

            if (courses == null)
            {
                return new List<GolfCourse>();
            }

            return courses;
        }

        public async Task<List<GolfCourse>> GetManagedGolfCoursesAsync()
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string userId = await TokenHelper.GetUserId();
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/golfcourse/owner/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<GolfCourse>();
            }

            var courses = await response.Content.ReadFromJsonAsync<List<GolfCourse>>();

            if (courses == null)
            {
                return new List<GolfCourse>();
            }

            return courses;
        }
    }
}
