using GolfApp.Helpers;
using GolfApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GolfApp.Services
{
    public sealed class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/user");

            if (!response.IsSuccessStatusCode)
            {
                return new List<User>();
            }

            var users = await response.Content.ReadFromJsonAsync<List<User>>();

            if (users == null)
            {
                return new List<User>();
            }

            return users;
        }

        public async Task<User> GetLoggedInUser()
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/user/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new User();
            }

            var user = await response.Content.ReadFromJsonAsync<User>();

            if (user == null)
            {
                return new User();
            }

            return user;
        }

        public async Task<bool> SetUserAvatar(string avatarUrl)
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            var id = await TokenHelper.GetUserId();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync($"/api/user/set-avatar?userId={id}&avatarUrl={avatarUrl}", null);

            return response.IsSuccessStatusCode;
        }
    }
}
