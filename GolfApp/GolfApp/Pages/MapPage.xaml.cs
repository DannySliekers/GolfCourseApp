using GolfApp.ViewModels;
using Microsoft.Maui.Maps;

namespace GolfApp.Pages;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _viewModel;
	public MapPage(MapViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.GeneratePins();
        try
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Denied", "Location permission is required to center the map.", "OK");
                return;
            }

            var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();

            if (location != null)
            {
                var userLocation = new Location(location.Latitude, location.Longitude);
                var mapSpan = MapSpan.FromCenterAndRadius(userLocation, Distance.FromKilometers(1));
                map.MoveToRegion(mapSpan);
            }
            else
            {
                await DisplayAlert("Location Error", "Could not determine location.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to get location: {ex.Message}", "OK");
        }
    }
}