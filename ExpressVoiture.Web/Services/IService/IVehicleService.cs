using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IVehicleService
    {

        Task<List<AdminVehicleListViewModel>> GetListAdminVehicleViewModel();
        Task<AddOrUpdateVehicleViewModel?> GetAddOrUpdateVehicleViewModel(int? id);
        Task SaveVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file);
        Task UpdateVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file);
        Task DeleteVoitureAVendre(int? id);
        Task<DeleteVehicleViewModel> GetDeleteVehicleViewModel(int? id);
    }
}
