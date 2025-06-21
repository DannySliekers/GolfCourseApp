using GolfApp.Models;
using GolfApp.ViewModels;

namespace GolfApp.Pages;

[QueryProperty(nameof(Booking), "Booking")]
public partial class BookTeeTimeDetailsPage : ContentPage
{
    private readonly BookTeeTimeDetailsViewModel _viewModel;
    private List<User> _allUsers;

    public BookTeeTimeDetailsPage(BookTeeTimeDetailsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    public Booking Booking
    {
        set
        {
            _viewModel.DetailBooking = value;
            _ = _viewModel.AddExistingUsers();
        }
    }

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        if (_viewModel.Users.Count < 4)
        {

            PopupOverlay.IsVisible = true;
            _allUsers = await ((BookTeeTimeDetailsViewModel)BindingContext).GetAllUsersAsync();
            var currentUser = await ((BookTeeTimeDetailsViewModel)BindingContext).GetLoggedInUserAsync();
            _allUsers.Remove(currentUser);
            UserSearchResults.ItemsSource = _allUsers;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "There are already 4 users  in your tee time", "ok");
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_allUsers == null) return;

        string search = e.NewTextValue?.ToLower() ?? "";
        var filtered = _allUsers.Where(u => u.UserName.ToLower().Contains(search)).ToList();
        UserSearchResults.ItemsSource = filtered;
    }

    private void OnUserSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is User selectedUser)
        {
            var viewModel = (BookTeeTimeDetailsViewModel)BindingContext;
            if (!viewModel.Users.Contains(selectedUser))
            {
                viewModel.Users.Add(selectedUser);
            }

            PopupOverlay.IsVisible = false;
        }
    }

    private void OnClosePopupClicked(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = false;
    }
}