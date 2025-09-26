using ExpressVoiture.MAUI.ViewModels;

namespace ExpressVoiture.MAUI.Views;

[QueryProperty(nameof(VoitureId), "voitureId")]
public partial class VehicleDetailsPage : ContentPage
{
    public int VoitureId { get; set; }

    private readonly VehicleDetailsViewModel _viewModel;

    public VehicleDetailsPage(VehicleDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (VoitureId != 0)
            await _viewModel.LoadVehicleAsync(VoitureId);
    }
}