using GestionVeterinaria.Dtos.Vacuna;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacunaController : ControllerBase
{
    private readonly IVacunaService _vacunaService;

    public VacunaController(IVacunaService vacunaService)
    {
        _vacunaService = vacunaService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VacunaDto>> ObtenerTodos()
    {
        var vacunas = _vacunaService.ObtenerTodos();
        return Ok(vacunas);
    }

    [HttpGet("{id}")]
    public ActionResult<VacunaDto> ObtenerPorId(int id)
    {
        var vacuna = _vacunaService.ObtenerPorId(id);
        if (vacuna == null)
        {
            return NotFound($"Vacuna con ID {id} no encontrada");
        }
        return Ok(vacuna);
    }

    [HttpGet("mascota/{mascotaId}")]
    public ActionResult<IEnumerable<VacunaDto>> ObtenerVacunasDeMascota(int mascotaId)
    {
        var vacunas = _vacunaService.ObtenerVacunasDeMascota(mascotaId);
        return Ok(vacunas);
    }

    [HttpPost]
    public ActionResult<bool> Crear([FromBody] CrearVacunaDto dto)
    {
        var resultado = _vacunaService.Crear(dto);
        if (!resultado)
        {
            return BadRequest("No se pudo crear la vacuna. Verifica que la mascota existe.");
        }
        return Ok(resultado);
    }

    [HttpPut]
    public ActionResult<bool> Actualizar([FromBody] ActualizarVacunaDto dto)
    {
        var resultado = _vacunaService.Actualizar(dto);
        if (!resultado)
        {
            return NotFound("Vacuna o mascota no encontrada");
        }
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Eliminar(int id)
    {
        var resultado = _vacunaService.Eliminar(id);
        if (!resultado)
        {
            return NotFound($"Vacuna con ID {id} no encontrada");
        }
        return Ok(resultado);
    }
}
