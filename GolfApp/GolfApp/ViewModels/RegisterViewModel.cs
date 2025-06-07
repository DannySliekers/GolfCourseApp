using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GolfApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string statusMessage;

        public RegisterViewModel()
        {
            var handler = new HttpClientHandler();

#if DEBUG
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
#endif
            _httpClient = new HttpClient(handler);
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "All fields are required.";
                return;
            }

            var registrationData = new
            {
                email = Email,
                username = Username,
                password = Password
            };

            var json = JsonSerializer.Serialize(registrationData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://10.0.2.2:7129/api/auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                    StatusMessage = "Registration successful!";
                }
                else
                {
                    StatusMessage = $"Registration failed: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                StatusMessage = $"Error: {ex.Message}";
            }
        }
    }
}
