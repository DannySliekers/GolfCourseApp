using GolfApp.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GolfApp.Services
{
    public sealed class GolfCourseService : IGolfCourseService
    {
        private readonly HttpClient _httpClient;

        public GolfCourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
