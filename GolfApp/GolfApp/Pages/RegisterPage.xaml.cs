using GolfApp.ViewModels;

namespace GolfApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}