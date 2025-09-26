using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.MAUI.Services.Interface
{
    public interface IVehicleAdminService
    {
        Task<List<AdminVehicleListDto>> GetVehiclesAsync();
        Task<bool> AddVehicleAsync(AddOrUpdateVehicleDto vehicle);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
