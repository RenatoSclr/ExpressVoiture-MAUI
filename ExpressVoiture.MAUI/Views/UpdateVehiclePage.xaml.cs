using ExpressVoiture.MAUI.ViewModels;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.MAUI.Views;

[QueryProperty(nameof(VoitureId), "voitureId")]
public partial class UpdateVehiclePage : ContentPage
{
    private readonly UpdateVehicleViewModel _viewModel;
    public int VoitureId { get; set; }
    public UpdateVehiclePage(UpdateVehicleViewModel viewModel)
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