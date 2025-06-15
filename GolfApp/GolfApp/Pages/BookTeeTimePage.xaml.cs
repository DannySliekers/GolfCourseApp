using GolfApp.Models;
using GolfApp.ViewModels;

namespace GolfApp.Pages;

[QueryProperty(nameof(GolfCourse), "GolfCourse")]
public partial class BookTeeTimePage : ContentPage
{
    private readonly BookTeeTimeViewModel _viewModel;

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
            _viewModel.GolfCourse = value;
            _viewModel.GenerateTeeTimes(value.BookingStartTime, value.BookingLastStartTime, value.StartTimeIntervalMinutes);
        }
    }
}