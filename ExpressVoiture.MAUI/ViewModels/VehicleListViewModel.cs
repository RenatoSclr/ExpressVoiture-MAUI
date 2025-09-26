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

        public VehicleListViewModel(IHomeService homeService, IAuthService authService, IUserStateService userStateService)
        {
            _homeService = homeService;
            _authService = authService;
            _userStateService = userStateService;

            // S'abonner aux changements d'état de connexion
            _userStateService.LoginStateChanged += OnLoginStateChanged;

            // Initialiser l'état
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
                Vehicles = new ObservableCollection<ClientVehicleListDto>(list);
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