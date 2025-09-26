using ExpressVoiture.MAUI.ViewModels;

namespace ExpressVoiture.MAUI.Views
{
    public partial class VehicleListPage : ContentPage
    {
        private readonly VehicleListViewModel _viewModel;

        public VehicleListPage(VehicleListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Vehicles == null || !_viewModel.Vehicles.Any())
            {
                await _viewModel.LoadVehiclesAsync();
            }
        }
    }
}