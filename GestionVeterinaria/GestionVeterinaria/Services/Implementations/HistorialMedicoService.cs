using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class HistorialMedicoService : IHistorialMedicoService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<HistorialMedico> _historialCrud;
    private readonly CrudGenerico<Mascota> _mascotaCrud;
    private readonly CrudGenerico<ServicioMedico> _servicioCrud;
    private readonly CrudGenerico<Veterinario> _veterinarioCrud;
    private readonly CrudGenerico<Tratamiento> _tratamientoCrud;

    public HistorialMedicoService(LiteDbContext context)
    {
        _context = context;
        _historialCrud = new CrudGenerico<HistorialMedico>(context, context.HistorialesMedicos);
        _mascotaCrud = new CrudGenerico<Mascota>(context, context.Mascotas);
        _servicioCrud = new CrudGenerico<ServicioMedico>(context, context.ServiciosMedicos);
        _veterinarioCrud = new CrudGenerico<Veterinario>(context, context.Veterinarios);
        _tratamientoCrud = new CrudGenerico<Tratamiento>(context, context.Tratamientos);
    }

    public HistorialMedicoDto? ObtenerPorId(int id)
    {
        var historial = _historialCrud.ObtenerPorId(id);
        if (historial == null) return null;

        var mascota = _mascotaCrud.ObtenerPorId(historial.MascotaId);
        if (mascota == null) return null;

        var serviciosMedicos = new List<ServicioMedicoDto>();
        foreach (var servicioId in historial.ServiciosMedicosId)
        {
            var servicio = _servicioCrud.ObtenerPorId(servicioId);
            if (servicio != null)
            {
                var veterinario = _veterinarioCrud.ObtenerPorId(servicio.VeterinariaId);
                var mascotaServicio = _mascotaCrud.ObtenerPorId(servicio.MascotaId);
                
                var tratamientos = new List<TratamientoDto>();
                foreach (var tId in servicio.TratamientosId)
                {
                    var t = _tratamientoCrud.ObtenerPorId(tId);
                    if (t != null)
                    {
                        tratamientos.Add(DTOMapper.MapTratamiento(t));
                    }
                }

                serviciosMedicos.Add(new ServicioMedicoDto
                {
                    ServicioMedicoId = servicio.ServicioMedicoId,
                    Precio = servicio.Precio,
                    Fecha = servicio.Fecha,
                    Descripcion = servicio.Descripcion,
                    VeterinarioDto = veterinario != null ? DTOMapper.MapVeterinario(veterinario) : null,
                    MascotaDto = mascotaServicio != null ? DTOMapper.MapMascota(mascotaServicio) : null,
                    TratamientoDtos = tratamientos
                });
            }
        }

        return new HistorialMedicoDto
        {
            HistorialMedicoId = historial.HistorialMedicoId,
            Fecha = historial.FechaCreacion,
            Mascota = DTOMapper.MapMascota(mascota),
            ServicioMedicoDtos = serviciosMedicos
        };
    }

    public IEnumerable<HistorialMedicoDto> ObtenerTodos()
    {
        var historiales = _historialCrud.ObtenerTodos().ToList();
        var lista = new List<HistorialMedicoDto>();

        foreach (var historial in historiales)
        {
            var mascota = _mascotaCrud.ObtenerPorId(historial.MascotaId);
            if (mascota == null) continue;

            var serviciosMedicos = new List<ServicioMedicoDto>();
            foreach (var servicioId in historial.ServiciosMedicosId)
            {
                var servicio = _servicioCrud.ObtenerPorId(servicioId);
                if (servicio != null)
                {
                    var veterinario = _veterinarioCrud.ObtenerPorId(servicio.VeterinariaId);
                    var mascotaServicio = _mascotaCrud.ObtenerPorId(servicio.MascotaId);
                    
                    var tratamientos = new List<TratamientoDto>();
                    foreach (var tId in servicio.TratamientosId)
                    {
                        var t = _tratamientoCrud.ObtenerPorId(tId);
                        if (t != null)
                        {
                            tratamientos.Add(DTOMapper.MapTratamiento(t));
                        }
                    }

                    serviciosMedicos.Add(new ServicioMedicoDto
                    {
                        ServicioMedicoId = servicio.ServicioMedicoId,
                        Precio = servicio.Precio,
                        Fecha = servicio.Fecha,
                        Descripcion = servicio.Descripcion,
                        VeterinarioDto = veterinario != null ? DTOMapper.MapVeterinario(veterinario) : null,
                        MascotaDto = mascotaServicio != null ? DTOMapper.MapMascota(mascotaServicio) : null,
                        TratamientoDtos = tratamientos
                    });
                }
            }

            lista.Add(new HistorialMedicoDto
            {
                HistorialMedicoId = historial.HistorialMedicoId,
                Fecha = historial.FechaCreacion,
                Mascota = DTOMapper.MapMascota(mascota),
                ServicioMedicoDtos = serviciosMedicos
            });
        }

        return lista;
    }

    public bool Crear(CrearHistorialMedicoDto dto)
    {
        var mascota = _mascotaCrud.ObtenerPorId(dto.MascotaId);
        if (mascota == null) return false;

        var historial = new HistorialMedico
        {
            MascotaId = dto.MascotaId,
            FechaCreacion = dto.FechaCreacion,
            ServiciosMedicosId = new List<int>()
        };

        return _historialCrud.Crear(historial);
    }

    public bool Eliminar(int id)
    {
        return _historialCrud.Eliminar(id);
    }

    public bool AddServicioMedico(int historialMedicoId, int servicioMedicoId)
    {
        var historial = _historialCrud.ObtenerPorId(historialMedicoId);
        if (historial == null) return false;

        var servicio = _servicioCrud.ObtenerPorId(servicioMedicoId);
        if (servicio == null) return false;

        if (!historial.ServiciosMedicosId.Contains(servicioMedicoId))
        {
            historial.ServiciosMedicosId.Add(servicioMedicoId);
            return _historialCrud.Actualizar(historial);
        }

        return false;
    }
}
