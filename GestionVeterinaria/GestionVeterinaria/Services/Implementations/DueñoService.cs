using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class DueñoService : IDueñoService
{
    private readonly LiteDbContext _context;

    public DueñoService(LiteDbContext context)
    {
        _context = context;
    }
    public DueñoDto? ObtenerPorId(int id)
    {
        var dueño = _context.Dueños.FindById(id);
        if (dueño == null)
        {
            return null;
        }
        
        var mascotasDtos = new List<MascotaDto>();

        foreach (var mascotaId in dueño.MascotasId)
        {
            var mascota = _context.Mascotas.FindById(mascotaId);
            if (mascota != null)
            {
                mascotasDtos.Add(new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie ,
                    Raza = mascota.Raza
                });
            }
        }

        return new DueñoDto
        {
            IdPersona = dueño.IdPersona,
            Nombre = dueño.Nombre,
            Edad = dueño.Edad,
            Direccion = dueño.Direccion,
            Telefono = dueño.Telefono,
            Mascotas = mascotasDtos
        };
    }

    public IEnumerable<DueñoDto> ObtenerTodos()
    {
        var dueños = _context.Dueños.FindAll().ToList();
        var dueñosDtos = new List<DueñoDto>();
        
        foreach (var dueño in dueños)
        {
            var mascotasDtos = new List<MascotaDto>();

            foreach (var mascotaId in dueño.MascotasId)
            {
                var mascota = _context.Mascotas.FindById(mascotaId);
                if (mascota != null)
                {
                    mascotasDtos.Add(new MascotaDto
                    {
                        IdMascota = mascota.IdMascota,
                        Nombre = mascota.Nombre,
                        Edad = mascota.Edad,
                        Peso = mascota.Peso,
                        Especie = mascota.Especie,
                        Raza = mascota.Raza
                    });
                }
            }

            dueñosDtos.Add(new DueñoDto
            {
                IdPersona = dueño.IdPersona,
                Nombre = dueño.Nombre,
                Edad = dueño.Edad,
                Direccion = dueño.Direccion,
                Telefono = dueño.Telefono,
                Mascotas = mascotasDtos
            });
        }
        
        return dueñosDtos;
    }

    public bool Crear(CrearDueñoDto dto)
    {
        var dueño = new Dueño
        {
            Nombre = dto.Nombre,
            Edad = dto.Edad,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            MascotasId = new List<int>() 
        };
        
        _context.Dueños.Insert(dueño);
        return true;
    }

    public bool Actualizar(ActualizarDueñoDto dto)
    {
        var dueño = _context.Dueños.FindById(dto.Id);
        if (dueño == null)
        {
            return false;
        }

        dueño.Nombre = dto.Nombre;
        dueño.Edad = dto.Edad;
        dueño.Direccion = dto.Direccion;
        dueño.Telefono = dto.Telefono;
        
        return _context.Dueños.Update(dueño);
    }

    public bool Eliminar(int id)
    {
        return _context.Dueños.Delete(id);
    }

    public IEnumerable<MascotaDto> ObtenerMascotasDeDueño(int dueñoId)
    {
        var dueño = _context.Dueños.FindById(dueñoId);
        if (dueño?.MascotasId == null)
        {
            return new List<MascotaDto>();
        }

        var mascotasDtos = new List<MascotaDto>();

        foreach (var mascotaId in dueño.MascotasId)
        {
            var mascota = _context.Mascotas.FindById(mascotaId);
            if (mascota != null)
            {
                mascotasDtos.Add(new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza
                });
            }
        }

        return mascotasDtos;
    }
}