using ExpressVoiture.API.Application.Services.Interface;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientVehiclesController : ControllerBase
{
    private readonly IVoitureService _voitureService;

    public ClientVehiclesController(IVoitureService voitureService)
    {
        _voitureService = voitureService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientVehicleListViewModel>>> GetAll()
    {
        var voitures = await _voitureService.GetAllClientVehicle(includeProperties: "Reparation,Vente");

        return Ok(voitures);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDetailedVehicleViewModel>> GetById(int id)
    {
        var vehicle = await _voitureService.GetClientDetailedVehicle(id);

        if (vehicle is null) return NotFound();

        return Ok(vehicle);
    }
}
