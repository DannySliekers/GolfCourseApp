using GolfApp.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GolfApp.Services
{
    public sealed class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/booking", content);

            return response.IsSuccessStatusCode;
        }
    }
}
