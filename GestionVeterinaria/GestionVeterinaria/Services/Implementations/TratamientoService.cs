using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class TratamientoService : ITratamientoService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Tratamiento> _tratamientoCrud;

    public TratamientoService(LiteDbContext context)
    {
        _context = context;
        _tratamientoCrud = new CrudGenerico<Tratamiento>(context, context.Tratamientos);
    }
    
    public TratamientoDto? ObtenerPorId(int id)
    {
        var tratamiento = _tratamientoCrud.ObtenerPorId(id);
        if (tratamiento == null) return null;

        return DTOMapper.MapTratamiento(tratamiento);
    }

    public IEnumerable<TratamientoDto> ObtenerTodos()
    {
        var tratamientos = _tratamientoCrud.ObtenerTodos().ToList();
        var lista = new List<TratamientoDto>();

        foreach (var tratamiento in tratamientos)
        {
            lista.Add(DTOMapper.MapTratamiento(tratamiento));
        }

        return lista;
    }

    public bool Crear(CrearTratamientoDto dto)
    {
        var tratamiento = new Tratamiento
        {
            NombreTratamiento = dto.NombreTratamiento,
            DescripcionTratamiento = dto.DescripcionTratamiento,
            TipoTratamiento = dto.TipoTratamiento,
        };
        return _tratamientoCrud.Crear(tratamiento);
    }

    public bool Actualizar(ActualizarTratamientoDto dto)
    {
        var tratamiento = _tratamientoCrud.ObtenerPorId(dto.TratamientoId);
        if (tratamiento == null)
        {
            return false;
        }

        tratamiento.NombreTratamiento = dto.NombreTratamiento;
        tratamiento.DescripcionTratamiento = dto.DescripcionTratamiento;
        tratamiento.TipoTratamiento = dto.TipoTratamiento;
        
        return _tratamientoCrud.Actualizar(tratamiento);
    }

    public bool Eliminar(int id)
    {
        return _tratamientoCrud.Eliminar(id);
    }
}
