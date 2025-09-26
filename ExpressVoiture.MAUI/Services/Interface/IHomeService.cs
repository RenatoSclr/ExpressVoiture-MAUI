using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.MAUI.Services.Interface
{
    public interface IHomeService
    {
        Task<List<ClientVehicleListDto>> GetAllClientVehicleListViewModelAsync();
        Task<ClientDetailedVehicleDto> GetClientDetailsViewModelAsync(int id);
    }
}
