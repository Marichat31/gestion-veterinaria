using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using LiteDB;

namespace GestionVeterinaria.Services.Implementations;

public class VeterinarioService : IVeterinarioService
{
    private readonly LiteDbContext _context;

    public VeterinarioService(LiteDbContext context)
    {
        _context = context;
    }

    public VeterinarioDTO? ObtenerPorId(int id)
    {
        var veterinario = _context.Veterinarios.FindById(id);
        if (veterinario == null)
        {
            return null;
        }

        var especialidadesDtos = new List<EspecialidadDto>();

        foreach (var especialidadId in veterinario.EspecialidadesId) 
        {
            var especialidad = _context.Especialidades.FindById(especialidadId);
            if (especialidad != null)
            {
                especialidadesDtos.Add(new EspecialidadDto
                {
                    IdEspecialidad = especialidad.EspecialidadId,
                    NombreEspecialidad = especialidad.Nombre,
                    DescripcionEspecialidad = especialidad.Descripcion

                });
            }
        }
        return new VeterinarioDTO
        {
            Id = veterinario.IdPersona,
            Nombre = veterinario.Nombre,
            Edad = veterinario.Edad,
            Direccion = veterinario.Direccion,
            Telefono = veterinario.Telefono,
            especialidades = especialidadesDtos,
        };
    }

    public IEnumerable<VeterinarioDTO> ObtenerTodos()
    {
        var veterinarios = _context.Veterinarios.FindAll().ToList();
        var veterinariosDtos = new List<VeterinarioDTO>();
        
        foreach (var veterinario in veterinarios)
        {
            var especialidadesDtos = new List<EspecialidadDto>();
            
            foreach (var especialidadId in veterinario.EspecialidadesId) 
            {
                var especialidad = _context.Especialidades.FindById(especialidadId);
                if (especialidad != null)
                {
                    especialidadesDtos.Add(new EspecialidadDto
                    {
                        IdEspecialidad = especialidad.EspecialidadId,
                        NombreEspecialidad = especialidad.Nombre,
                        DescripcionEspecialidad = especialidad.Descripcion

                    });
                }
            }
            veterinariosDtos.Add(new VeterinarioDTO
            {
                Id= veterinario.IdPersona,
                Nombre = veterinario.Nombre,
                Edad = veterinario.Edad,
                Direccion = veterinario.Direccion,
                Telefono = veterinario.Telefono,
                especialidades = especialidadesDtos
            });
        }

        return veterinariosDtos;
    }

    public IEnumerable<EspecialidadDto> ObtenerEspecialidadDeVeterinario(int veterinarioId)
    {
        var veterinario = _context.Veterinarios.FindById(veterinarioId);
        if (veterinario?.EspecialidadesId == null)
        {
            return new List<EspecialidadDto>();
        }

        var especialidadesDtos = new List<EspecialidadDto>();

        foreach (var especialidadId in veterinario.EspecialidadesId)
        {
            var especialidad = _context.Especialidades.FindById(especialidadId);
            if (especialidad != null)
            {
                especialidadesDtos.Add(new EspecialidadDto()
                {
                    IdEspecialidad = especialidad.EspecialidadId,
                    NombreEspecialidad = especialidad.Nombre,
                    DescripcionEspecialidad = especialidad.Descripcion
                });
            }
        }

        return especialidadesDtos;
    }

    public bool Crear(CrearVeterinarioDTO dto)
    {

        var veterinario = new Veterinario
        {
            Nombre = dto.Nombre,
            Edad = dto.Edad,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            EspecialidadesId = dto.IdEspecialidades
        };
        _context.Veterinarios.Insert(veterinario);
    return true;
}
    public bool Actualizar(ActualizarVeterinarioDTO dto)
    {
        var veterinario = _context.Veterinarios.FindById(dto.Id);
        if (veterinario == null)
        {
            return false;
        }

        veterinario.Nombre = dto.Nombre;
        veterinario.Edad = dto.Edad;
        veterinario.Direccion = dto.Direccion;
        veterinario.Telefono = dto.Telefono;
        
        return _context.Veterinarios.Update(veterinario);
    }
    public bool Eliminar(int id)
    {
        return _context.Veterinarios.Delete(id);
    }
}