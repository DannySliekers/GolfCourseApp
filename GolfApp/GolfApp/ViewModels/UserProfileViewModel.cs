using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Services;

namespace GolfApp.ViewModels
{
    public partial class UserProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private User _user;
        private readonly IUserService _userService;
        private readonly IUploadService _uploadService;

        public UserProfileViewModel(IUserService userService, IUploadService uploadService) 
        {
            _userService = userService;
            _uploadService = uploadService;
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            SecureStorage.Remove("jwt");
            (Shell.Current as AppShell).HandleLogout();
            await Shell.Current.GoToAsync("//Login");
        }

        [RelayCommand]
        private async Task ChangeAvatar()
        {
            FileResult photo = null;

            var action = await Application.Current.MainPage.DisplayActionSheet(
                "Choose photo source",
                "Cancel",
                null,
                "Camera",
                "Gallery");

            if (action == "Camera")
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    photo = await MediaPicker.Default.CapturePhotoAsync();
                }
            }
            else if (action == "Gallery")
            {
                photo = await MediaPicker.Default.PickPhotoAsync();
            }

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                var localPath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using var fileStream = File.OpenWrite(localPath);
                await stream.CopyToAsync(fileStream);
                var avatarUrl = await _uploadService.UploadFileAsync(stream, photo.FileName);
                await _userService.SetUserAvatar(UrlHelpers.TransformToLocalHost(avatarUrl));
    
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    avatarUrl = UrlHelpers.TransformPortToHttpAndroid(avatarUrl);
                }
                else
                {
                    avatarUrl = UrlHelpers.TransformPortToHttp(avatarUrl);
                }

                User = new User
                {
                    Id = User.Id,
                    Email = User.Email,
                    UserName = User.UserName,
                    AvatarUrl = avatarUrl,
                };
            }
        }

        public async Task SetUserAsync()
        {
            var user = await _userService.GetLoggedInUser();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                user.AvatarUrl = UrlHelpers.TransformUrl(user.AvatarUrl);
            }
            else
            {
                user.AvatarUrl = UrlHelpers.TransformPortToHttp(user.AvatarUrl);
            }

            User = user;
        }
    }
}
