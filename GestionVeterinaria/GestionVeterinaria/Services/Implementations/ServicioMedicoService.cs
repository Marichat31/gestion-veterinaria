using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class ServicioMedicoService : IServicioMedicoService
{
    private readonly LiteDbContext _context;

    public ServicioMedicoService(LiteDbContext context)
    {
        _context = context;
    }

    public ServicioMedicoDto? ObtenerPorId(int id)
    {
        var servicio = _context.ServiciosMedicos.FindById(id);
        if (servicio == null) return null;

        var veterinario = _context.Veterinarios.FindById(servicio.VeterinariaId);
        var mascota = _context.Mascotas.FindById(servicio.MascotaId);
        var tratamientos = new List<TratamientoDto>();
        foreach (var tId in servicio.TratamientosId)
        {
            var t = _context.Tratamientos.FindById(tId);
            if (t != null)
            {
                /*
                tratamientos.Add(new TratamientoDto
                {
                     
                });*/
            }
        }

        return new ServicioMedicoDto
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
                IdMascota = mascota.IdMascota,
                Nombre = mascota.Nombre,
                Edad = mascota.Edad,
                Peso = mascota.Peso,
                Especie = mascota.Especie,
                Raza = mascota.Raza
            },
            TratamientoDtos = tratamientos
        };
    }

    public IEnumerable<ServicioMedicoDto> ObtenerTodos()
    {
        var servicios = _context.ServiciosMedicos.FindAll().ToList();
        var lista = new List<ServicioMedicoDto>();

        foreach (var servicio in servicios)
        {
            var veterinario = _context.Veterinarios.FindById(servicio.VeterinariaId);
            var mascota = _context.Mascotas.FindById(servicio.MascotaId);
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

            lista.Add(new ServicioMedicoDto
            {
                ServicioMedicoId = servicio.ServicioMedicoId,
                Precio = servicio.Precio,
                Fecha = servicio.Fecha,
                Descripcion = servicio.Descripcion,
                VeterinarioDto = new VeterinarioDTO
                {
                    Id = veterinario.IdPersona,
                    Nombre = veterinario.Nombre,
                    Edad = veterinario.Edad,
                    Direccion = veterinario.Direccion,
                    Telefono = veterinario.Telefono
                },
                MascotaDto = new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza
                },
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
            TratamientosId = new List<int>()
        };
        _context.ServiciosMedicos.Insert(servicio);
        return true;
    }

    public bool Actualizar(ActualizarServicioMedicoDto dto)
    {
        var servicio = _context.ServiciosMedicos.FindById(dto.VeterinarioId);
        if (servicio == null) return false;

        servicio.Precio = dto.Precio;
        servicio.Descripcion = dto.Descripcion;
        servicio.VeterinariaId = dto.VeterinarioId;

        return _context.ServiciosMedicos.Update(servicio);
    }

    public bool Eliminar(int id)
    {
        return _context.ServiciosMedicos.Delete(id);
    }
}
