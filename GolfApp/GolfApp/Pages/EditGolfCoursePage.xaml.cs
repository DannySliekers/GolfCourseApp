using GolfApp.Models;
using GolfApp.ViewModels;

namespace GolfApp.Pages;

[QueryProperty(nameof(GolfCourse), "GolfCourse")]
public partial class EditGolfCoursePage : ContentPage
{
	private readonly EditGolfCourseViewModel _viewModel;

	public EditGolfCoursePage(EditGolfCourseViewModel viewModel)
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
            _viewModel.FirstTeeTime = value.BookingStartTime.ToTimeSpan();
            _viewModel.LastTeeTime = value.BookingLastStartTime.ToTimeSpan();
            _viewModel.TeeTimeIntervalMinutes = value.StartTimeIntervalMinutes;
        }
    }
}