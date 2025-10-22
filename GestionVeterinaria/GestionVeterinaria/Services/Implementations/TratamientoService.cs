using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class TratamientoService : ITratamientoService
{
    private readonly LiteDbContext _context;
    public TratamientoService(LiteDbContext context)
    {
        _context = context;
    }
    
    public TratamientoDto? ObtenerPorId(int id)
    {
        var tratamiento = _context.Tratamientos.FindById(id);
        if (tratamiento == null) return null;
        

        return new TratamientoDto
        {
            TratamientoId = tratamiento.TratamientoId,
            NombreTratamiento = tratamiento.NombreTratamiento,
            TipoTratamiento = tratamiento.TipoTratamiento,
            DescripcionTratamiento = tratamiento.DescripcionTratamiento,
        };
    }

    public IEnumerable<TratamientoDto> ObtenerTodos()
    {
        var tratamientos = _context.Tratamientos.FindAll().ToList();
        var lista = new List<TratamientoDto>();

        foreach (var tratamiento in tratamientos)
        {
            lista.Add(new TratamientoDto
            {
                TratamientoId = tratamiento.TratamientoId,
                NombreTratamiento = tratamiento.NombreTratamiento,
                TipoTratamiento = tratamiento.TipoTratamiento,
                DescripcionTratamiento = tratamiento.DescripcionTratamiento,
            });
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
        _context.Tratamientos.Insert(tratamiento);
        return true;
    }

    public bool Actualizar(ActualizarTratamientoDto dto)
    {
        var tratamiento = _context.Tratamientos.FindById(dto.TratamientoId);
        if (tratamiento == null)
        {
            return false;
        }

        tratamiento.NombreTratamiento = dto.NombreTratamiento;
        tratamiento.DescripcionTratamiento = dto.DescripcionTratamiento;
        tratamiento.TipoTratamiento = dto.TipoTratamiento;
        return true;
    }

    public bool Eliminar(int id)
    {
        return _context.Tratamientos.FindById(id) != null;
    }
}