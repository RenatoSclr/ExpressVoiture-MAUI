using ExpressVoiture.MAUI.ViewModels;

namespace ExpressVoiture.MAUI.Views;

public partial class AdminVehicleListPage : ContentPage
{
    private readonly VehicleAdminListViewModel _viewModel;

    public AdminVehicleListPage(VehicleAdminListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_viewModel.Vehicles.Any())
            await _viewModel.LoadVehiclesAsync();
    }
}