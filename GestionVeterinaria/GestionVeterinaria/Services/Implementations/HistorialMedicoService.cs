using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class HistorialMedicoService : IHistorialMedicoService
{
    private readonly LiteDbContext _context;

    public HistorialMedicoService(LiteDbContext context)
    {
        _context = context;
    }

    public HistorialMedicoDto? ObtenerPorId(int id)
    {
        var historial = _context.HistorialesMedicos.FindById(id);
        if (historial == null) return null;

        var mascota = _context.Mascotas.FindById(historial.MascotaId);
        if (mascota == null) return null;

        var serviciosMedicos = new List<ServicioMedicoDto>();
        foreach (var servicioId in historial.ServiciosMedicosId)
        {
            var servicio = _context.ServiciosMedicos.FindById(servicioId);
            if (servicio != null)
            {
                var veterinario = _context.Veterinarios.FindById(servicio.VeterinariaId);
                var mascotaServicio = _context.Mascotas.FindById(servicio.MascotaId);
                
                var tratamientos = new List<TratamientoDto>();
                foreach (var tId in servicio.TratamientosId)
                {
                    var t = _context.Tratamientos.FindById(tId);
                    if (t != null)
                    {
                        /*
                        tratamientos.Add(new TratamientoDto
                        {
                            IdTratamiento = t.IdTratamiento,
                            Nombre = t.Nombre,
                            Descripcion = t.Descripcion
                        });*/
                    }
                }

                serviciosMedicos.Add(new ServicioMedicoDto
                {
                    ServicioMedicoId = servicio.ServicioMedicoId,
                    Precio = servicio.Precio,
                    Fecha = servicio.Fecha,
                    Descripcion = servicio.Descripcion,
                    VeterinarioDto = new VeterinarioDTO
                    {
                        Id= veterinario.IdPersona,
                        Nombre = veterinario.Nombre,
                        Edad = veterinario.Edad,
                        Direccion = veterinario.Direccion,
                        Telefono = veterinario.Telefono
                    },
                    MascotaDto = new MascotaDto
                    {
                        IdMascota = mascotaServicio.IdMascota,
                        Nombre = mascotaServicio.Nombre,
                        Edad = mascotaServicio.Edad,
                        Peso = mascotaServicio.Peso,
                        Especie = mascotaServicio.Especie,
                        Raza = mascotaServicio.Raza
                    },
                    TratamientoDtos = tratamientos
                });
            }
        }

        return new HistorialMedicoDto
        {
            HistorialMedicoId = historial.HistorialMedicoId,
            Fecha = historial.FechaCreacion,
            Mascota = new MascotaDto
            {
                IdMascota = mascota.IdMascota,
                Nombre = mascota.Nombre,
                Edad = mascota.Edad,
                Peso = mascota.Peso,
                Especie = mascota.Especie,
                Raza = mascota.Raza
            },
            ServicioMedicoDtos = serviciosMedicos
        };
    }

    public IEnumerable<HistorialMedicoDto> ObtenerTodos()
    {
        var historiales = _context.HistorialesMedicos.FindAll().ToList();
        var lista = new List<HistorialMedicoDto>();

        foreach (var historial in historiales)
        {
            var mascota = _context.Mascotas.FindById(historial.MascotaId);
            if (mascota == null) continue;

            var serviciosMedicos = new List<ServicioMedicoDto>();
            foreach (var servicioId in historial.ServiciosMedicosId)
            {
                var servicio = _context.ServiciosMedicos.FindById(servicioId);
                if (servicio != null)
                {
                    var veterinario = _context.Veterinarios.FindById(servicio.VeterinariaId);
                    var mascotaServicio = _context.Mascotas.FindById(servicio.MascotaId);
                    
                    var tratamientos = new List<TratamientoDto>();
                    foreach (var tId in servicio.TratamientosId)
                    {
                        var t = _context.Tratamientos.FindById(tId);
                        if (t != null)
                        {
                            /*
                            tratamientos.Add(new TratamientoDto
                            {
                                IdTratamiento = t.IdTratamiento,
                                Nombre = t.Nombre,
                                Descripcion = t.Descripcion
                            });*/
                        }
                    }

                    serviciosMedicos.Add(new ServicioMedicoDto
                    {
                        ServicioMedicoId = servicio.ServicioMedicoId,
                        Precio = servicio.Precio,
                        Fecha = servicio.Fecha,
                        Descripcion = servicio.Descripcion,
                        VeterinarioDto = new VeterinarioDTO
                        {
                            Id= veterinario.IdPersona,
                            Nombre = veterinario.Nombre,
                            Edad = veterinario.Edad,
                            Direccion = veterinario.Direccion,
                            Telefono = veterinario.Telefono
                        },
                        MascotaDto = new MascotaDto
                        {
                            IdMascota = mascotaServicio.IdMascota,
                            Nombre = mascotaServicio.Nombre,
                            Edad = mascotaServicio.Edad,
                            Peso = mascotaServicio.Peso,
                            Especie = mascotaServicio.Especie,
                            Raza = mascotaServicio.Raza
                        },
                        TratamientoDtos = tratamientos
                    });
                }
            }

            lista.Add(new HistorialMedicoDto
            {
                HistorialMedicoId = historial.HistorialMedicoId,
                Fecha = historial.FechaCreacion,
                Mascota = new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza
                },
                ServicioMedicoDtos = serviciosMedicos
            });
        }

        return lista;
    }

    public bool Crear(CrearHistorialMedicoDto dto)
    {
        var mascota = _context.Mascotas.FindById(dto.MascotaId);
        if (mascota == null) return false;

        var historial = new HistorialMedico
        {
            MascotaId = dto.MascotaId,
            FechaCreacion = dto.FechaCreacion,
            ServiciosMedicosId = new List<int>()
        };

        _context.HistorialesMedicos.Insert(historial);
        return true;
    }

    public bool Eliminar(int id)
    {
        return _context.HistorialesMedicos.Delete(id);
    }

    public bool AddServicioMedico(int historialMedicoId, int servicioMedicoId)
    {
        var historial = _context.HistorialesMedicos.FindById(historialMedicoId);
        if (historial == null) return false;

        var servicio = _context.ServiciosMedicos.FindById(servicioMedicoId);
        if (servicio == null) return false;

        if (!historial.ServiciosMedicosId.Contains(servicioMedicoId))
        {
            historial.ServiciosMedicosId.Add(servicioMedicoId);
            return _context.HistorialesMedicos.Update(historial);
        }

        return false;
    }
}