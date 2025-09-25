using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.API.Application.Services.Interface
{
    public interface IVoitureService
    {
        Task<List<AdminVehicleListViewModel>> GetListAdminVehicleViewModel();
        Task<AddOrUpdateVehicleViewModel> GetAddOrUpdateVehicleViewModel(int? id);
        Task SaveVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter);
        Task UpdateVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter);
        Task DeleteVoitureAVendre(int? id);
        Task<VoitureAVendre> GetVoitureAVendreById(int? id);
        Task<DeleteVehicleViewModel> GetDeleteVehicleViewModel(int? id);
        Task<ClientDetailedVehicleViewModel> GetClientDetailedVehicle(int id);
        Task<IEnumerable<ClientVehicleListViewModel>> GetAllClientVehicle(string includeProperties);
    }
}