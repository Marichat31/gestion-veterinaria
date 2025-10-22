using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionVeterinaria.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VeterinarioController : ControllerBase 
{
    private readonly IVeterinarioService _service;
    
    public VeterinarioController(IVeterinarioService service)
    {
        _service = service;
    }
    [HttpGet]
    public ActionResult<IEnumerable<VeterinarioDTO>> GetAll()
    {
        var veterinarios = _service.ObtenerTodos();
        return Ok(veterinarios);
    }
    [HttpGet("{id}")]
    public ActionResult<VeterinarioDTO> GetById(int id)
    {
        var veterinario = _service.ObtenerPorId(id);
        if (veterinario == null)
        {
            return NotFound();
        }
        return Ok(veterinario);
    }
    
    [HttpGet("{id:int}/especialidades")]
    public ActionResult<IEnumerable<EspecialidadDto>> GetEspecialidades(int id)
    {
        var veterinario = _service.ObtenerPorId(id);
        if (veterinario == null)
        {
            return NotFound();
        }
        
        var especialidades = _service.ObtenerEspecialidadDeVeterinario(id);
        return Ok(especialidades);
    }
    
    [HttpPost]
    public ActionResult Create(CrearVeterinarioDTO dto)
    {
        var resultado = _service.Crear(dto);
        if (!resultado)
        {
            return BadRequest();
        }
        return Ok();
    }
    [HttpPut("{id}")]
    public ActionResult Update(ActualizarVeterinarioDTO dto)
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