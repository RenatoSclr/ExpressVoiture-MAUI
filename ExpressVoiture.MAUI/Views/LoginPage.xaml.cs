using ExpressVoiture.MAUI.ViewModels;

namespace ExpressVoiture.MAUI.Views
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}