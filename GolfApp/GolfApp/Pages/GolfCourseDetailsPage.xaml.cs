using GolfApp.Models;
using GolfApp.ViewModels;

namespace GolfApp.Pages;

[QueryProperty(nameof(GolfCourse), "GolfCourse")]
public partial class GolfCourseDetailsPage : ContentPage
{
	private readonly GolfCourseDetailsViewModel _viewModel;

	public GolfCourseDetailsPage(GolfCourseDetailsViewModel viewModel)
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

            if (value != null)
            {
                _ = _viewModel.LoadImagesAsync(value.Id);
            }
        }
    }
}