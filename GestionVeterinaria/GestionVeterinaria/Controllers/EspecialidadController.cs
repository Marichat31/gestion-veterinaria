using GestionVeterinaria.Dtos;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspecialidadController : ControllerBase
{
    private readonly IEspecialidadService _service;
    public EspecialidadController(IEspecialidadService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DueñoDto>> GetAll()
    {
        var especialidades = _service.ObtenerTodos();
        return Ok(especialidades);
    }
    
    [HttpGet("{id}")]
    public ActionResult<EspecialidadDto> GetById(int id)
    {
        var especialidad = _service.ObtenerPorId(id);
        if (especialidad == null)
        {
            return NotFound();
        }
        return Ok(especialidad);
    }
    
    [HttpPost]
    public ActionResult Create(CrearEspecialidadDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(ActualizarEspecialidadDto dto)
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