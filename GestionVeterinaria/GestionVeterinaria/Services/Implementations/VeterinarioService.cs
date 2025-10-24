using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;
using LiteDB;

namespace GestionVeterinaria.Services.Implementations;

public class VeterinarioService : IVeterinarioService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Veterinario> _veterinarioCrud;
    private readonly CrudGenerico<Especialidad> _especialidadCrud;

    public VeterinarioService(LiteDbContext context)
    {
        _context = context;
        _veterinarioCrud = new CrudGenerico<Veterinario>(context, context.Veterinarios);
        _especialidadCrud = new CrudGenerico<Especialidad>(context, context.Especialidades);
    }

    public VeterinarioDTO? ObtenerPorId(int id)
    {
        var veterinario = _veterinarioCrud.ObtenerPorId(id);
        if (veterinario == null)
        {
            return null;
        }

        var especialidadesDtos = new List<EspecialidadDto>();

        foreach (var especialidadId in veterinario.EspecialidadesId) 
        {
            var especialidad = _especialidadCrud.ObtenerPorId(especialidadId);
            if (especialidad != null)
            {
                especialidadesDtos.Add(DTOMapper.MapEspecialidad(especialidad));
            }
        }
        
        var veterinarioDto = DTOMapper.MapVeterinario(veterinario);
        veterinarioDto.especialidades = especialidadesDtos;
        
        return veterinarioDto;
    }

    public IEnumerable<VeterinarioDTO> ObtenerTodos()
    {
        var veterinarios = _veterinarioCrud.ObtenerTodos().ToList();
        var veterinariosDtos = new List<VeterinarioDTO>();
        
        foreach (var veterinario in veterinarios)
        {
            var especialidadesDtos = new List<EspecialidadDto>();
            
            foreach (var especialidadId in veterinario.EspecialidadesId) 
            {
                var especialidad = _especialidadCrud.ObtenerPorId(especialidadId);
                if (especialidad != null)
                {
                    especialidadesDtos.Add(DTOMapper.MapEspecialidad(especialidad));
                }
            }
            
            var veterinarioDto = DTOMapper.MapVeterinario(veterinario);
            veterinarioDto.especialidades = especialidadesDtos;
            veterinariosDtos.Add(veterinarioDto);
        }

        return veterinariosDtos;
    }

    public IEnumerable<EspecialidadDto> ObtenerEspecialidadDeVeterinario(int veterinarioId)
    {
        var veterinario = _veterinarioCrud.ObtenerPorId(veterinarioId);
        if (veterinario?.EspecialidadesId == null)
        {
            return new List<EspecialidadDto>();
        }

        var especialidadesDtos = new List<EspecialidadDto>();

        foreach (var especialidadId in veterinario.EspecialidadesId)
        {
            var especialidad = _especialidadCrud.ObtenerPorId(especialidadId);
            if (especialidad != null)
            {
                especialidadesDtos.Add(DTOMapper.MapEspecialidad(especialidad));
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
        return _veterinarioCrud.Crear(veterinario);
    }

    public bool Actualizar(ActualizarVeterinarioDTO dto)
    {
        var veterinario = _veterinarioCrud.ObtenerPorId(dto.Id);
        if (veterinario == null)
        {
            return false;
        }

        veterinario.Nombre = dto.Nombre;
        veterinario.Edad = dto.Edad;
        veterinario.Direccion = dto.Direccion;
        veterinario.Telefono = dto.Telefono;
        
        return _veterinarioCrud.Actualizar(veterinario);
    }

    public bool Eliminar(int id)
    {
        return _veterinarioCrud.Eliminar(id);
    }
}
