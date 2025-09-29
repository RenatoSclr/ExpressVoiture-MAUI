using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.MAUI.Views;
using ExpressVoiture.Shared.ViewModel;
using System.Collections.ObjectModel;

namespace ExpressVoiture.MAUI.ViewModels
{
    public partial class VehicleListViewModel : ObservableObject
    {
        private readonly IHomeService _homeService;
        private readonly IAuthService _authService;
        private readonly IUserStateService _userStateService;

        [ObservableProperty]
        private ObservableCollection<ClientVehicleListDto> vehicles = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool isLoggedIn = false;

        [ObservableProperty]
        private string loginButtonText = "Connexion";

        private List<ClientVehicleListDto> allVehicles = new();

        [ObservableProperty]
        private string searchText;


        public VehicleListViewModel(IHomeService homeService, IAuthService authService, IUserStateService userStateService)
        {
            _homeService = homeService;
            _authService = authService;
            _userStateService = userStateService;

            _userStateService.LoginStateChanged += OnLoginStateChanged;

            IsLoggedIn = _userStateService.IsLoggedIn;
            UpdateLoginButtonText();
        }

        private void OnLoginStateChanged(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
            UpdateLoginButtonText();
        }

        [RelayCommand]
        public async Task LoadVehiclesAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var list = await _homeService.GetAllClientVehicleListViewModelAsync();
                allVehicles = list.ToList();
                Vehicles = new ObservableCollection<ClientVehicleListDto>(allVehicles);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur",
                    $"Impossible de charger les véhicules : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void FilterVehicles()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Vehicles = new ObservableCollection<ClientVehicleListDto>(allVehicles);
            }
            else
            {
                var lower = SearchText.ToLowerInvariant();
                var filtered = allVehicles.Where(v =>
                    (v.Marque?.ToLower().Contains(lower) ?? false) ||
                    (v.Modele?.ToLower().Contains(lower) ?? false) ||
                    (v.Finition?.ToLower().Contains(lower) ?? false) ||
                    v.Annee.ToString().Contains(lower) 
                ).ToList();

                Vehicles = new ObservableCollection<ClientVehicleListDto>(filtered);
            }
        }

        [RelayCommand]
        public async Task ToggleLoginAsync()
        {
            if (IsLoggedIn)
            {
                await _authService.LogoutAsync();
                _userStateService.SetLoginState(false);
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        [RelayCommand]
        public async Task GoToAdminAsync()
        {
            if (IsLoggedIn)
            {
                await Shell.Current.GoToAsync(nameof(AdminVehicleListPage));
            }
        }

        private void UpdateLoginButtonText()
        {
            LoginButtonText = IsLoggedIn ? "Déconnexion" : "Connexion";
        }

        [RelayCommand]
        public async Task VehicleSelectedAsync(ClientVehicleListDto vehicle)
        {
            if (vehicle is null) return;

            await Shell.Current.GoToAsync($"{nameof(VehicleDetailsPage)}?voitureId={vehicle.VoitureId}");
        }
    }
}
