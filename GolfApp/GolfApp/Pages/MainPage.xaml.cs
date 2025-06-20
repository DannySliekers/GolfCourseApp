using GolfApp.Models;
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
        await _viewModel.LoadUserRoleAndSetVisibility();
    }

    private async void OnGolfCourseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is GolfCourse selectedCourse)
        {
            ((CollectionView)sender).SelectedItem = null;

            await Shell.Current.GoToAsync("GolfCourseDetails",
                new Dictionary<string, object>
                {
                    { "GolfCourse", selectedCourse }
                });
        }
    }
}

