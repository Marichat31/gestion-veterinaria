using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class EspecialidadService : IEspecialidadService
{
    private readonly LiteDbContext _context;

    public EspecialidadService(LiteDbContext context)
    {
        _context = context;
    }

    public EspecialidadDto? ObtenerPorId(int id)
    {
        var especialidad = _context.Especialidades.FindById(id);
        if (especialidad == null)
        {
            return null;
        }

        return new EspecialidadDto
        {
            IdEspecialidad = especialidad.EspecialidadId,
            NombreEspecialidad = especialidad.Nombre,
            DescripcionEspecialidad = especialidad.Descripcion
        };
    }

    public IEnumerable<EspecialidadDto> ObtenerTodos()
    {
        var especialidades = _context.Especialidades.FindAll().ToList();
        var especialidadDtos = new List<EspecialidadDto>();

        foreach (var especialidad in especialidades)
        {
            especialidadDtos.Add(new  EspecialidadDto
            {
               IdEspecialidad = especialidad.EspecialidadId,
               NombreEspecialidad = especialidad.Nombre,
               DescripcionEspecialidad = especialidad.Descripcion
            });
        }
        return especialidadDtos;
    }

    public bool Crear(CrearEspecialidadDto dto)
    {
        var veterinario = _context.Veterinarios.FindById(dto.VeterinarioId);
        if (veterinario == null)
        {
            return false;
        }
        var especialidad = new Especialidad
        {
            Nombre = dto.NombreEspecialidad,
            Descripcion = dto.DescripcionEspecialidad,
            VeterinarioId = dto.VeterinarioId
        };
        _context.Especialidades.Insert(especialidad);

        if (!veterinario.EspecialidadesId.Contains(especialidad.EspecialidadId))
        {
            veterinario.EspecialidadesId.Add(especialidad.EspecialidadId);
            _context.Veterinarios.Update(veterinario);
        } 
        return true;
    }

    public bool Actualizar(ActualizarEspecialidadDto dto)
    {
        var especialidad = _context.Especialidades.FindById(dto.Id);
        if (especialidad == null)
        {
            return false;
        }

        especialidad.Nombre = dto.NombreEspecialidad;
        especialidad.Descripcion = dto.DescripcionEspecialidad;   
        return _context.Especialidades.Update(especialidad);
    }
    public bool Eliminar(int id)
    {
        return _context.Especialidades.Delete(id);
    }
}