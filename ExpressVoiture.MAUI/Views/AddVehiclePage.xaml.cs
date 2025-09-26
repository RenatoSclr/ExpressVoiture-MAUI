using ExpressVoiture.MAUI.ViewModels;

namespace ExpressVoiture.MAUI.Views;

public partial class AddVehiclePage : ContentPage
{
	public AddVehiclePage(AddVehicleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}