using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.MAUI.Views;
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

        [RelayCommand]
        public async Task CreateVehicleAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));
        }

        [RelayCommand]
        public async Task DeleteVehicleAsync(AdminVehicleListDto vehicle)
        {
            if (vehicle == null) return;

            // Demande confirmation
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                $"Voulez-vous vraiment supprimer {vehicle.Marque} {vehicle.Modele} ?",
                "Oui", "Non");

            if (!confirm)
                return;

            IsBusy = true;
            try
            {
                var success = await _vehicleService.DeleteVehicleAsync(vehicle.VoitureId);

                if (success)
                {
                    Vehicles.Remove(vehicle);
                    await Shell.Current.DisplayAlert("Succès", "Véhicule supprimé avec succès.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "La suppression a échoué.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
