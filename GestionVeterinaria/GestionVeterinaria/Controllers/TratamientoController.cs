using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TratamientoController : ControllerBase
{
    private readonly ITratamientoService _service;
    
    public TratamientoController(ITratamientoService service)
    {
        _service = service;
    }
    [HttpGet]
    public ActionResult<IEnumerable<TratamientoDto>> GetAll()
    {
        var servicios = _service.ObtenerTodos();
        return Ok(servicios);
    }
    
    [HttpGet("{id}")]
    public ActionResult<TratamientoDto> GetById(int id)
    {
        var servicio = _service.ObtenerPorId(id);
        if (servicio == null)
        {
            return NotFound();
        }
        return Ok(servicio);
    }
    
    [HttpPost]
    public ActionResult Create(CrearTratamientoDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(ActualizarTratamientoDto dto)
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