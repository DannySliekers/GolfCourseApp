using GolfApp.Helpers;
using GolfApp.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/booking", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/booking?id={bookingId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddUserToBookingAsync(int bookingId)
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string userId = await TokenHelper.GetUserId();

            HttpResponseMessage response = await _httpClient.PostAsync($"/api/booking/{bookingId}/users/{userId}", null);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Booking>> GetAllBookingAsync()
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("/api/booking");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Booking>();
            }

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();

            if (bookings == null)
            {
                return new List<Booking>();
            }

            return bookings;
        }

        public async Task<List<Booking>> GetUserBookingsAsync()
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string userId = await TokenHelper.GetUserId();

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/booking/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Booking>();
            }

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();

            if (bookings == null)
            {
                return new List<Booking>();
            }

            return bookings;
        }

        public async Task<List<int>> GetUserIdsForBookingAsync(int bookingId)
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/booking/{bookingId}/users");

            if (!response.IsSuccessStatusCode)
            {
                return new List<int>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var userIds = JsonSerializer.Deserialize<List<int>>(content);
            return userIds ?? new List<int>();
        }

        public async Task<bool> RemoveUserFromBookingAsync(int bookingId, int userId)
        {
            string token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/booking/{bookingId}/users/{userId}");

            return response.IsSuccessStatusCode;
        }
    }
}
