using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class UserProfilePage : ContentPage
{
    private UserProfileViewModel _viewModel;

	public UserProfilePage(UserProfileViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.SetUser();
    }

}