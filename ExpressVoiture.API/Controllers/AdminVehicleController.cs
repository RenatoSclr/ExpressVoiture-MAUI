using ExpressVoiture.API.Application.Services.Interface;
using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVoitureService _voitureService;

    public VehiclesController(IVoitureService voitureService)
    {
        _voitureService = voitureService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AdminVehicleListDto>>> GetAll()
    {
        var voitures = await _voitureService.GetListAdminVehicleViewModel();

        return Ok(voitures);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddOrUpdateVehicleDto>> GetById(int id)
    {
        var vehicle = await _voitureService.GetAddOrUpdateVehicleViewModel(id);
        if (vehicle == null) return NotFound();

        return Ok(vehicle);
    }

    [HttpGet("delete/{id}")]
    public async Task<ActionResult<DeleteVehicleDto>> GetDeleteVehicleById(int id)
    {
        var vehicle = await _voitureService.GetDeleteVehicleViewModel(id);
        if (vehicle == null) return NotFound();

        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVoitureAVendreById(int id)
    {
        await _voitureService.DeleteVoitureAVendre(id);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> AddVoitureAVendre([FromBody] AddOrUpdateVehicleDto voiture)
    {
        await _voitureService.SaveVoitureAVendre(voiture);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateVoitureAVendre([FromBody] AddOrUpdateVehicleDto voiture)
    {
        await _voitureService.UpdateVoitureAVendre(voiture);

        return Ok();
    }

    [HttpGet("VoitureAVendre/{id}")]
    public async Task<ActionResult<VoitureAVendre>> GetVoitureAVendreById(int id)
    {
        var vehicle = await _voitureService.GetVoitureAVendreById(id);
        if (vehicle == null) return NotFound();

        return Ok(vehicle);
    }
}
