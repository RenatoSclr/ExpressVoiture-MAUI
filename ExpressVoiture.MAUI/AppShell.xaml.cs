using ExpressVoiture.MAUI.Views;

namespace ExpressVoiture.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VehicleDetailsPage), typeof(VehicleDetailsPage));
            Routing.RegisterRoute(nameof(AdminVehicleListPage), typeof(AdminVehicleListPage));
            Routing.RegisterRoute(nameof(AddVehiclePage), typeof(AddVehiclePage));
        }
    }
}
