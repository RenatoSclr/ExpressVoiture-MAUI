using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IFileService
    {
        AddOrUpdateVehicleViewModel CreateFile(AddOrUpdateVehicleViewModel voiture, IFormFile file);
        void DeleteFileByVehiculeId(int id);
    }
}
