using ExpressVoiture.Domain.Models;
using ExpressVoiture.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IVehicleService
    {

        List<AdminVehicleListViewModel> GetListAdminVehicleViewModel();
        double CalculateSellPrice(double coutReparation, double prixAxhat);
        AddOrUpdateVehicleViewModel GetAddOrUpdateVehicleViewModel(int? id);
        void SaveVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file);
        void UpdateVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file);
        void DeleteVoitureAVendre(int? id);
        VoitureAVendre GetVoitureAVendreById(int? id);
        DeleteVehicleViewModel GetDeleteVehicleViewModel(int? id);
    }
}
