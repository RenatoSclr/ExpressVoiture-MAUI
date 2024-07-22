using ExpressVoiture.DataAccess.Repository;
using ExpressVoiture.Domain.IRepository;
using ExpressVoiture.Domain.Models;
using ExpressVoiture.Services;
using ExpressVoiture.Services.IService;
using ExpressVoiture.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace ExpressVoiture.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }   
        public IActionResult Index()
        {
            List<AdminVehicleListViewModel> voitureList = _vehicleService.GetListAdminVehicleViewModel();
            return View(voitureList);
        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View(new AddOrUpdateVehicleViewModel());
            }
            else
            {
                AddOrUpdateVehicleViewModel voitureAModifier = _vehicleService.GetAddOrUpdateVehicleViewModel(id);
                return View(voitureAModifier);
            }

        }

        [HttpPost]
        public IActionResult Upsert(AddOrUpdateVehicleViewModel voiture, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
               

                if (voiture.VoitureId == 0)
                {
                    _vehicleService.SaveVoitureAVendre(voiture, file);
                    TempData["success"] = "Votre véhicule a bien été ajoutée";
                }
                else
                {
                    _vehicleService.UpdateVoitureAVendre(voiture, file);
                    TempData["success"] = "Votre véhicule a bien été éditer";
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                return View(voiture);
            }

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            DeleteVehicleViewModel? voiture = _vehicleService.GetDeleteVehicleViewModel(id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            
            if (ModelState.IsValid)
            {
                _vehicleService.DeleteVoitureAVendre(id);
                TempData["success"] = "Votre véhicule a bien été supprimé";

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
