using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class ServicioMedicoService : IServicioMedicoService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<ServicioMedico> _servicioCrud;
    private readonly CrudGenerico<Veterinario> _veterinarioCrud;
    private readonly CrudGenerico<Mascota> _mascotaCrud;
    private readonly CrudGenerico<Tratamiento> _tratamientoCrud;
    private readonly CrudGenerico<HistorialMedico> _historialMedicoCrud;

    public ServicioMedicoService(LiteDbContext context)
    {
        _context = context;
        _servicioCrud = new CrudGenerico<ServicioMedico>(context, context.ServiciosMedicos);
        _veterinarioCrud = new CrudGenerico<Veterinario>(context, context.Veterinarios);
        _mascotaCrud = new CrudGenerico<Mascota>(context, context.Mascotas);
        _tratamientoCrud = new CrudGenerico<Tratamiento>(context, context.Tratamientos);
        _historialMedicoCrud = new CrudGenerico<HistorialMedico>(context, context.HistorialesMedicos);
    }

    public ServicioMedicoDto? ObtenerPorId(int id)
    {
        var servicio = _servicioCrud.ObtenerPorId(id);
        if (servicio == null) return null;

        var veterinario = _veterinarioCrud.ObtenerPorId(servicio.VeterinariaId);
        var mascota = _mascotaCrud.ObtenerPorId(servicio.MascotaId);
        var tratamientos = new List<TratamientoDto>();
        foreach (var tId in servicio.TratamientosId)
        {
            var t = _tratamientoCrud.ObtenerPorId(tId);
            if (t != null)
            {
                tratamientos.Add(DTOMapper.MapTratamiento(t));
            }
        }

        return new ServicioMedicoDto
        {
            ServicioMedicoId = servicio.ServicioMedicoId,
            Precio = servicio.Precio,
            Fecha = servicio.Fecha,
            Descripcion = servicio.Descripcion,
            VeterinarioDto = veterinario != null ? DTOMapper.MapVeterinario(veterinario) : null,
            MascotaDto = mascota != null ? DTOMapper.MapMascota(mascota) : null,
            TratamientoDtos = tratamientos
        };
    }

    public IEnumerable<ServicioMedicoDto> ObtenerTodos()
    {
        var servicios = _servicioCrud.ObtenerTodos().ToList();
        var lista = new List<ServicioMedicoDto>();

        foreach (var servicio in servicios)
        {
            var veterinario = _veterinarioCrud.ObtenerPorId(servicio.VeterinariaId);
            var mascota = _mascotaCrud.ObtenerPorId(servicio.MascotaId);
            var tratamientos = new List<TratamientoDto>();
            foreach (var tId in servicio.TratamientosId)
            {
                var t = _tratamientoCrud.ObtenerPorId(tId);
                if (t != null)
                {
                    tratamientos.Add(DTOMapper.MapTratamiento(t));
                }
            }

            lista.Add(new ServicioMedicoDto
            {
                ServicioMedicoId = servicio.ServicioMedicoId,
                Precio = servicio.Precio,
                Fecha = servicio.Fecha,
                Descripcion = servicio.Descripcion,
                VeterinarioDto = veterinario != null ? DTOMapper.MapVeterinario(veterinario) : null,
                MascotaDto = mascota != null ? DTOMapper.MapMascota(mascota) : null,
                TratamientoDtos = tratamientos
            });
        }

        return lista;
    }

    public bool Crear(CrearServicioMedicoDto dto)
    {
        var servicio = new ServicioMedico
        {
            Precio = dto.Precio,
            Fecha = dto.Fecha,
            Descripcion = dto.Descripcion,
            VeterinariaId = dto.VeterinarioId,
            MascotaId = dto.MascotaId,
            TratamientosId = dto.TratamientosId
        };
        
        var creado = _servicioCrud.Crear(servicio);
        if (!creado) return false;

        var historialesMascota = _historialMedicoCrud.ObtenerTodos()
            .Where(h => h.MascotaId == servicio.MascotaId)
            .ToList();

        foreach (var historial in historialesMascota)
        {
            if (!historial.ServiciosMedicosId.Contains(servicio.ServicioMedicoId))
            {
                historial.ServiciosMedicosId.Add(servicio.ServicioMedicoId);
                _historialMedicoCrud.Actualizar(historial);
            }
        }

        return true;
    }

    public bool Actualizar(ActualizarServicioMedicoDto dto)
    {
        var servicio = _servicioCrud.ObtenerPorId(dto.VeterinarioId);
        if (servicio == null) return false;

        servicio.Precio = dto.Precio;
        servicio.Descripcion = dto.Descripcion;
        servicio.VeterinariaId = dto.VeterinarioId;

        return _servicioCrud.Actualizar(servicio);
    }

    public bool Eliminar(int id)
    {
        return _servicioCrud.Eliminar(id);
    }
}
