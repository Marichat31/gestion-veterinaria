using GestionVeterinaria.Dtos;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;

[ApiController]
[Route("api/duenos")]
public class DueñosController : ControllerBase
{
    private readonly IDueñoService _service;

    public DueñosController(IDueñoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DueñoDto>> GetAll()
    {
        var dueños = _service.ObtenerTodos();
        return Ok(dueños);
    }

    [HttpGet("{id:int}")]
    public ActionResult<DueñoDto> GetById(int id)
    {
        var dueño = _service.ObtenerPorId(id);
        if (dueño == null)
        {
            return NotFound();
        }
        return Ok(dueño);
    }

    [HttpGet("{id:int}/mascotas")]
    public ActionResult<IEnumerable<MascotaDto>> GetMascotas(int id)
    {
        var dueño = _service.ObtenerPorId(id);
        if (dueño == null)
        {
            return NotFound();
        }
        
        var mascotas = _service.ObtenerMascotasDeDueño(id);
        return Ok(mascotas);
    }

    [HttpPost]
    public ActionResult Create(CrearDueñoDto dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(ActualizarDueñoDto dto)
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