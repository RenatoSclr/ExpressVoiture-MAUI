using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpressVoiture.MAUI.Services.Interface;

namespace ExpressVoiture.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IUserStateService _userStateService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        public LoginViewModel(IAuthService authService, IUserStateService userStateService)
        {
            _authService = authService;
            _userStateService = userStateService;
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                bool success = await _authService.LoginAsync(Email, Password);

                if (success)
                {
                    _userStateService.SetLoginState(true);

                    await Shell.Current.GoToAsync("//VehicleListPage");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Une erreur est survenue : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("//VehicleListPage");
        }
    }
}