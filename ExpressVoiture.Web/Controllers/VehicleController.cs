using ExpressVoiture.Services.IService;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class VehicleController : Controller
{
    private readonly IVehicleService _vehicleService;
    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        var voitureList = await _vehicleService.GetListAdminVehicleViewModel();
        return View(voitureList);
    }

    public async Task<IActionResult> Upsert(int? id)
    {
        if (id == null || id == 0)
            return View(new AddOrUpdateVehicleDto());

        var voiture = await _vehicleService.GetAddOrUpdateVehicleViewModel(id);
        return View(voiture);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(AddOrUpdateVehicleDto voiture, IFormFile? file)
    {
        if (!ModelState.IsValid)
            return View(voiture);

        if (voiture.VoitureId == 0)
        {
            await _vehicleService.SaveVoitureAVendre(voiture, file);
            TempData["success"] = "Votre véhicule a bien été ajoutée";
        }
        else
        {
            await _vehicleService.UpdateVoitureAVendre(voiture, file);
            TempData["success"] = "Votre véhicule a bien été édité";
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var voiture = await _vehicleService.GetDeleteVehicleViewModel(id);
        if (voiture == null) return NotFound();

        return View(voiture);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(int? id)
    {
        if (ModelState.IsValid)
        {
            await _vehicleService.DeleteVoitureAVendre(id);
            TempData["success"] = "Votre véhicule a bien été supprimé";
            return RedirectToAction("Index");
        }

        return View();
    }
}
