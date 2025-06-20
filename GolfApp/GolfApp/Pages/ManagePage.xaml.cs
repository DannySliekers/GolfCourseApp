using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class ManagePage : ContentPage
{
    private readonly ManageViewModel _viewModel;

	public ManagePage(ManageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetManagedGolfCourses();
    }
}