using GolfApp.Models;
using GolfApp.ViewModels;

namespace GolfApp.Pages;

[QueryProperty(nameof(GolfCourse), "GolfCourse")]
public partial class BookTeeTimePage : ContentPage
{
    private readonly BookTeeTimeViewModel _viewModel;
    private GolfCourse _golfCourse;

    public BookTeeTimePage(BookTeeTimeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public GolfCourse GolfCourse
    {
        set
        {
            _golfCourse = value;
            _viewModel.GolfCourse = value;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_golfCourse != null)
        {
            await _viewModel.GenerateTeeTimes(
                _golfCourse.BookingStartTime,
                _golfCourse.BookingLastStartTime,
                _golfCourse.StartTimeIntervalMinutes);
        }
    }
}