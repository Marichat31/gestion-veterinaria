using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialesMedicosController : ControllerBase
{
    private readonly IHistorialMedicoService _service;

    public HistorialesMedicosController(IHistorialMedicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<HistorialMedicoDto>> GetAll()
    {
        var historiales = _service.ObtenerTodos();
        return Ok(historiales);
    }

    [HttpGet("{id}")]
    public ActionResult<HistorialMedicoDto> GetById(int id)
    {
        var historial = _service.ObtenerPorId(id);
        if (historial == null)
        {
            return NotFound();
        }
        return Ok(historial);
    }
    [HttpGet("{id:int}/historial-completo")]
    public ActionResult<HistorialMedicoDto> GetHistorialCompleto(int id)
    {
        var resultado = _service.ObtenerHistorialCompleto(id);
        if (resultado == null)
            return NotFound($"No se encontró el historial de la mascota con ID {id}");

        return Ok(resultado);
    }

    [HttpPost]
    public ActionResult Create(CrearHistorialMedicoDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var resultado = _service.Eliminar(id);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{historialMedicoId}/servicios/{servicioMedicoId}")]
    public ActionResult AddServicioMedico(int historialMedicoId, int servicioMedicoId)
    {
        var resultado = _service.AddServicioMedico(historialMedicoId, servicioMedicoId);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }
}