using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public MainPage(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
}

