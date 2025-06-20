using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class ManagePage : ContentPage
{
	public ManagePage(ManageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}