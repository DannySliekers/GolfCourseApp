using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class AddGolfCoursePage : ContentPage
{
	public AddGolfCoursePage(AddGolfCourseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}