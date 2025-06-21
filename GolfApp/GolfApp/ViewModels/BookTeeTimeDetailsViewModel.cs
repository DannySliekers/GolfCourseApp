using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Models;
using GolfApp.Services;
using System.Collections.ObjectModel;

namespace GolfApp.ViewModels
{
    public partial class BookTeeTimeDetailsViewModel : ObservableObject
    {
        public ObservableCollection<User> Users { get; set; } = [];

        [ObservableProperty]
        private Booking detailBooking;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private List<int> existingUsers;

        public BookTeeTimeDetailsViewModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        [RelayCommand]
        private async Task BookTeeTime()
        {
            if (DetailBooking.Id != 0)
            {
                bool allSucceeded = true;

                foreach (var user in Users)
                {
                    if (!existingUsers.Contains(user.Id))
                    {
                        var success = await _bookingService.AddUserToBookingAsync(DetailBooking.Id, user.Id);

                        if (!success)
                        {
                            allSucceeded = false;
                            break;
                        }
                    }
                }

                if (allSucceeded)
                {
                    await Shell.Current.DisplayAlert("Success", "Added user(s) to booking.", "OK");
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to add one or more users to booking.", "OK");
                }
            }
            else
            {
                var booking = await _bookingService.AddBookingAsync(DetailBooking);

                if (booking.Id != 0)
                {
                    bool allSucceeded = true;

                    foreach (var user in Users)
                    {
                        if (user.Id != booking.CreatedByUserId)
                        {
                            var userToBookingSuccess = await _bookingService.AddUserToBookingAsync(booking.Id, user.Id);

                            if (!userToBookingSuccess)
                            {
                                allSucceeded = false;
                                break;
                            }
                        }
                    }

                    if (allSucceeded)
                    {
                        await Shell.Current.DisplayAlert("Success", "Added booking", "OK");
                        await Shell.Current.GoToAsync("//MainPage");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to add one or more users to booking.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to add booking.", "OK");
                }
            }
        }

        public async Task AddExistingUsers()
        {
            existingUsers = await _bookingService.GetUserIdsForBookingAsync(DetailBooking.Id);
            var allUsers = await _userService.GetAllUsers();

            foreach (var userId in existingUsers)
            {
                Users.Add(allUsers.FirstOrDefault(x => x.Id == userId));
            }

            var currentUser = await _userService.GetLoggedInUser();

            if (!existingUsers.Contains(currentUser.Id))
            {
                Users.Add(currentUser);
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsers();
        }

        public async Task<User> GetLoggedInUserAsync()
        {
            return await _userService.GetLoggedInUser();
        }
    }
}
