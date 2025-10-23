using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class EspecialidadService : IEspecialidadService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Especialidad> _especialidadCrud;
    private readonly CrudGenerico<Veterinario> _veterinarioCrud;

    public EspecialidadService(LiteDbContext context)
    {
        _context = context;
        _especialidadCrud = new CrudGenerico<Especialidad>(context, context.Especialidades);
        _veterinarioCrud = new CrudGenerico<Veterinario>(context, context.Veterinarios);
    }

    public EspecialidadDto? ObtenerPorId(int id)
    {
        var especialidad = _especialidadCrud.ObtenerPorId(id);
        if (especialidad == null)
        {
            return null;
        }

        return DTOMapper.MapEspecialidad(especialidad);
    }

    public IEnumerable<EspecialidadDto> ObtenerTodos()
    {
        var especialidades = _especialidadCrud.ObtenerTodos().ToList();
        var especialidadDtos = new List<EspecialidadDto>();

        foreach (var especialidad in especialidades)
        {
            especialidadDtos.Add(DTOMapper.MapEspecialidad(especialidad));
        }
        return especialidadDtos;
    }

    public bool Crear(CrearEspecialidadDto dto)
    { 
        /*var veterinario = _veterinarioCrud.ObtenerPorId(dto.VeterinarioId);
        if (veterinario == null)
        {
            return false;
        }*/
        
        var especialidad = new Especialidad
        {
            Nombre = dto.NombreEspecialidad,
            Descripcion = dto.DescripcionEspecialidad,
            //VeterinarioId = dto.VeterinarioId
        };
        _especialidadCrud.Crear(especialidad);
/*
        if (!veterinario.EspecialidadesId.Contains(especialidad.EspecialidadId))
        {
            veterinario.EspecialidadesId.Add(especialidad.EspecialidadId);
            _veterinarioCrud.Actualizar(veterinario);
        } */
        return true;
    }

    public bool Actualizar(ActualizarEspecialidadDto dto)
    {
        var especialidad = _especialidadCrud.ObtenerPorId(dto.Id);
        if (especialidad == null)
        {
            return false;
        }

        especialidad.Nombre = dto.NombreEspecialidad;
        especialidad.Descripcion = dto.DescripcionEspecialidad;   
        return _especialidadCrud.Actualizar(especialidad);
    }
    
    public bool Eliminar(int id)
    {
        return _especialidadCrud.Eliminar(id);
    }
}
