using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ExpressVoiture.API.Application.Services.Interface
{
    public interface IVoitureService
    {
        Task<List<AdminVehicleListDto>> GetListAdminVehicleViewModel();
        Task<AddOrUpdateVehicleDto> GetAddOrUpdateVehicleViewModel(int? id);
        Task SaveVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter, IFormFile formFile);
        Task UpdateVoitureAVendre(AddOrUpdateVehicleDto voitureAAjouter);
        Task DeleteVoitureAVendre(int? id);
        Task<VoitureAVendre> GetVoitureAVendreById(int? id);
        Task<DeleteVehicleDto> GetDeleteVehicleViewModel(int? id);
        Task<ClientDetailedVehicleDto> GetClientDetailedVehicle(int id);
        Task<IEnumerable<ClientVehicleListDto>> GetAllClientVehicle(string includeProperties);
    }
}