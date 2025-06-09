using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Services;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace GolfApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string statusMessage;

        private readonly IAuthService _authService;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
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

            try
            {
                var success = await _authService.RegisterAsync(Email, Username, Password);
                StatusMessage = success ? "Registration successful!" : "Registration failed.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }
    }
}
