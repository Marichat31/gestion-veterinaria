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
        var especialidad = new Especialidad
        {
            Nombre = dto.NombreEspecialidad,
            Descripcion = dto.DescripcionEspecialidad
        };
        _context.Especialidades.Insert(especialidad);
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