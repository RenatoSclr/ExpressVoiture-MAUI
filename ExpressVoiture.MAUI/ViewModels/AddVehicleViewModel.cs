using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.Shared.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoiture.MAUI.ViewModels
{
    public partial class AddVehicleViewModel : ObservableObject
    {
        private readonly IVehicleAdminService _vehicleAdminService;

        [ObservableProperty]
        private AddOrUpdateVehicleDto vehicle = new();

        [ObservableProperty]
        private bool isBusy;

        public AddVehicleViewModel(IVehicleAdminService vehicleAdminService)
        {
            _vehicleAdminService = vehicleAdminService;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var context = new ValidationContext(Vehicle);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(Vehicle, context, results, true);

                if (!isValid)
                {
                    var errorMessage = string.Join("\n", results.Select(r => r.ErrorMessage));
                    await Shell.Current.DisplayAlert("Validation", errorMessage, "OK");
                    return; 
                }

                var success = await _vehicleAdminService.AddVehicleAsync(Vehicle);

                if (success)
                {
                    await Shell.Current.DisplayAlert("Succès", "Véhicule ajouté avec succès.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erreur", "Impossible d’ajouter le véhicule.", "OK");
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
