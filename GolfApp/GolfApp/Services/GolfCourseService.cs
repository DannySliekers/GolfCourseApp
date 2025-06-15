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

        public async Task<bool> AddGolfCourseAsync(GolfCourse course)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(course);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/golfcourse", content);

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
    }
}
