using ExpressVoiture.MAUI.ViewModels;
using ExpressVoiture.Shared.ViewModel;

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

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is ClientVehicleListDto vehicle)
            {
                await ((VehicleListViewModel)BindingContext).VehicleSelectedAsync(vehicle);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is VehicleListViewModel vm)
            {
                vm.FilterVehicles();
            }
        }
    }
}