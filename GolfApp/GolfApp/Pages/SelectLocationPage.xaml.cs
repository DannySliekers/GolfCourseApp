using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace GolfApp.Pages;

public partial class SelectLocationPage : ContentPage
{
    private readonly Action<Location> _onLocationSelected;
    private Pin currentPin;

    public SelectLocationPage(Action<Location> onLocationSelected)
    {
        InitializeComponent();
        _onLocationSelected = onLocationSelected;
    }

    private async void OnSetLocationClicked(object sender, EventArgs e)
    {
        var center = map.VisibleRegion?.Center;

        if (center != null)
        {
            _onLocationSelected?.Invoke(currentPin.Location);
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Couldn't get center of map.", "OK");
        }
    }

    private void map_MapClicked(object sender, MapClickedEventArgs e)
    {
        map.Pins.Clear();
        currentPin = new Pin()
        {
            Label = $"Golf course location",
            Location = new Location(e.Location.Latitude, e.Location.Longitude),
            Type = PinType.Place
        };
        map.Pins.Add(currentPin);
        SetLocationButton.IsEnabled = true;
    }
}