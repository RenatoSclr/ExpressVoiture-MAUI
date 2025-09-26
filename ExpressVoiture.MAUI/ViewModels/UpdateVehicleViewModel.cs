using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.MAUI.ViewModels
{
    public partial class UpdateVehicleViewModel : ObservableObject
    {
        private readonly IVehicleAdminService _vehicleAdminService;

        [ObservableProperty]
        private AddOrUpdateVehicleDto vehicle = new();

        [ObservableProperty]
        private bool isBusy;

        public UpdateVehicleViewModel(IVehicleAdminService vehicleAdminService)
        {
            _vehicleAdminService = vehicleAdminService;
        }

        public async Task LoadVehicleAsync(int vehicleId)
        {
            if (vehicleId <= 0) return;

            try
            {
                IsBusy = true;
                var loadedVehicle = await _vehicleAdminService.GetVehicleByIdAsync(vehicleId);
                if (loadedVehicle != null)
                {
                    Vehicle = loadedVehicle;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "Impossible de charger le véhicule.", "OK");
                    await Shell.Current.GoToAsync("..");
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

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var success = await _vehicleAdminService.UpdateVehicleAsync(Vehicle);

                if (success)
                {
                    await Shell.Current.DisplayAlert("Succès", "Véhicule mis à jour avec succès.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "Impossible de mettre à jour le véhicule.", "OK");
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
