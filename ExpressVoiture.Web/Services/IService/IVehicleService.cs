using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IVehicleService
    {

        Task<List<AdminVehicleListDto>> GetListAdminVehicleViewModel();
        Task<AddOrUpdateVehicleDto?> GetAddOrUpdateVehicleViewModel(int? id);
        Task SaveVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter, IFormFile file);
        Task UpdateVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter, IFormFile file);
        Task DeleteVoitureAVendre(int? id);
        Task<DeleteVehicleDto> GetDeleteVehicleViewModel(int? id);
    }
}
