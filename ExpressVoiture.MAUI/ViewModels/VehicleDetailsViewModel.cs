using CommunityToolkit.Mvvm.ComponentModel;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.MAUI.ViewModels;

public partial class VehicleDetailsViewModel : ObservableObject
{
    private readonly IHomeService _homeService;

    [ObservableProperty]
    private ClientDetailedVehicleDto? vehicle;

    [ObservableProperty]
    private bool isBusy;

    public VehicleDetailsViewModel(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public async Task LoadVehicleAsync(int id)
    {
        try
        {
            IsBusy = true;
            Vehicle = await _homeService.GetClientDetailsViewModelAsync(id);
        }
        finally
        {
            IsBusy = false;
        }
    }
}