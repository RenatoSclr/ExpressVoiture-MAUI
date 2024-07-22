using ExpressVoiture.Domain.Models;
using ExpressVoiture.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IFileService
    {
        AddOrUpdateVehicleViewModel CreateFile(AddOrUpdateVehicleViewModel voiture, IFormFile file);
        VoitureAVendre DeleteFile(VoitureAVendre voiture);
    }
}
