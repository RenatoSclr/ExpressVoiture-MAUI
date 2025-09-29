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

        private List<AdminVehicleListDto> allVehicles = new();

        [ObservableProperty]
        private string searchText;

        public IEnumerable<VehicleSortOption> SortOptions =>
            Enum.GetValues(typeof(VehicleSortOption)).Cast<VehicleSortOption>();


        [ObservableProperty]
        private VehicleSortOption selectedSortOption = VehicleSortOption.None;

        public VehicleAdminListViewModel(IVehicleAdminService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [RelayCommand]
        public async Task LoadVehiclesAsync()
        {
            IsBusy = true;
            var list = await _vehicleService.GetVehiclesAsync();
            allVehicles = list.ToList();
            Vehicles = new ObservableCollection<AdminVehicleListDto>(allVehicles);
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

        [RelayCommand]
        public async Task UpdateVehicleAsync(AdminVehicleListDto vehicle)
        {
            if (vehicle is null) return;

            await Shell.Current.GoToAsync($"{nameof(UpdateVehiclePage)}?voitureId={vehicle.VoitureId}");
        }

        public void FilterVehicles()
        {
            IEnumerable<AdminVehicleListDto> filtered = allVehicles;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var lower = SearchText.ToLowerInvariant();
                filtered = filtered.Where(v =>
                    (v.Marque?.ToLower().Contains(lower) ?? false) ||
                    (v.Modele?.ToLower().Contains(lower) ?? false) ||
                    (v.Finition?.ToLower().Contains(lower) ?? false) ||
                    v.Annee.ToString().Contains(lower)
                );
            }

            filtered = SelectedSortOption switch
            {
                VehicleSortOption.AnneeAsc => filtered.OrderBy(v => v.Annee),
                VehicleSortOption.AnneeDesc => filtered.OrderByDescending(v => v.Annee),
                VehicleSortOption.PrixAsc => filtered.OrderBy(v => v.PrixVente),
                VehicleSortOption.PrixDesc => filtered.OrderByDescending(v => v.PrixVente),
                VehicleSortOption.DateVenteAsc => filtered.OrderBy(v => v.DateVente),
                VehicleSortOption.DateVenteDesc => filtered.OrderByDescending(v => v.DateVente),
                _ => filtered
            };

            Vehicles = new ObservableCollection<AdminVehicleListDto>(filtered);
        }

        partial void OnSelectedSortOptionChanged(VehicleSortOption value)
        {
            FilterVehicles();
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterVehicles();
        }
    }
}
