using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;
using System.Collections.ObjectModel;

namespace ExpressVoiture.MAUI.ViewModels
{
    public partial class VehicleAdminListViewModel : ObservableObject
    {
        private readonly IVehicleAdminService _vehicleService;

        [ObservableProperty]
        private ObservableCollection<AdminVehicleListDto> vehicles = new();

        [ObservableProperty]
        private bool isBusy;

        public VehicleAdminListViewModel(IVehicleAdminService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [RelayCommand]
        public async Task LoadVehiclesAsync()
        {
            IsBusy = true;
            var list = await _vehicleService.GetVehiclesAsync();
            Vehicles = new ObservableCollection<AdminVehicleListDto>(list);
            IsBusy = false;
        }
    }
}
