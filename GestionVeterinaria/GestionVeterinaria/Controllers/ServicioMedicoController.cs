using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiciosMedicosController : ControllerBase
{
    private readonly IServicioMedicoService _service;

    public ServiciosMedicosController(IServicioMedicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ServicioMedicoDto>> GetAll()
    {
        var servicios = _service.ObtenerTodos();
        return Ok(servicios);
    }

    [HttpGet("{id}")]
    public ActionResult<ServicioMedicoDto> GetById(int id)
    {
        var servicio = _service.ObtenerPorId(id);
        if (servicio == null)
        {
            return NotFound();
        }
        return Ok(servicio);
    }

    [HttpPost]
    public ActionResult Create(CrearServicioMedicoDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(ActualizarServicioMedicoDto dto)
    {
        var resultado = _service.Actualizar(dto);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
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
}