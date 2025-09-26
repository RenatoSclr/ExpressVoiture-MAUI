using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.API.Application.Services.Interface
{
    public interface IVoitureService
    {
        Task<List<AdminVehicleListDto>> GetListAdminVehicleViewModel();
        Task<AddOrUpdateVehicleDto> GetAddOrUpdateVehicleViewModel(int? id);
        Task SaveVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter);
        Task UpdateVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter);
        Task DeleteVoitureAVendre(int? id);
        Task<VoitureAVendre> GetVoitureAVendreById(int? id);
        Task<DeleteVehicleDto> GetDeleteVehicleViewModel(int? id);
        Task<ClientDetailedVehicleDto> GetClientDetailedVehicle(int id);
        Task<IEnumerable<ClientVehicleListDto>> GetAllClientVehicle(string includeProperties);
    }
}