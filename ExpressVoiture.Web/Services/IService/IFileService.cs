using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.Services.IService
{
    public interface IFileService
    {
        AddOrUpdateVehicleDto CreateFile(AddOrUpdateVehicleDto voiture, IFormFile file);
        void DeleteFileByVehiculeId(int id);
    }
}
