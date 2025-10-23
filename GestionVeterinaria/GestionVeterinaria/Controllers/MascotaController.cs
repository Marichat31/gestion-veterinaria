using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Mascota;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MascotasController : ControllerBase
{
    private readonly IMascotaService _service;

    public MascotasController(IMascotaService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MascotaDto>> GetAll()
    {
        var mascotas = _service.ObtenerTodos();
        return Ok(mascotas);
    }

    [HttpGet("{id:int}")]
    public ActionResult<MascotaDto> GetById(int id)
    {
        var mascota = _service.ObtenerPorId(id);
        if (mascota == null)
        {
            return NotFound();
        }
        return Ok(mascota);
    }
    [HttpGet("VacunasVencidas")]
    public ActionResult<IEnumerable<MascotaDto>> GetMascotasConVacunasVencidas()
    {
        try
        {
            var resultado = _service.MascotasConVacunaVencida();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
    [HttpGet("Buscar")]
    public ActionResult<IEnumerable<MascotaDto>> FilterForAge(
        [FromQuery] string especie, 
        [FromQuery] int edadMin, 
        [FromQuery] int edadMax)
    {
        var resultado = _service.FiltroPorEspecieyRango(especie, edadMin, edadMax);
        return Ok(resultado);
    }
    [HttpPost]
    public ActionResult Create(CrearMascotaDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(ActualizarMascotaDto dto)
    {
        var resultado = _service.Actualizar(dto);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var resultado = _service.Eliminar(id);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("dueño/{idDueño:int}/mascota/{idMascota:int}")]
    public ActionResult AgregarMascota(int idDueño, int idMascota)
    {
        var resultado = _service.AgregarMascota(idDueño, idMascota);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }
}