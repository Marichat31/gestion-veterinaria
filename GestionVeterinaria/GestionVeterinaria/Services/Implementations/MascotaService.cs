using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.Mascota;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;

namespace GestionVeterinaria.Services.Implementations;

public class MascotaService : IMascotaService
{
    private readonly LiteDbContext _context;

    public MascotaService(LiteDbContext context)
    {
        _context = context;
    }

    public MascotaDto? ObtenerPorId(int id)
    {
        var mascota = _context.Mascotas.FindById(id);
        if (mascota == null) return null;

        var dueño = _context.Dueños.FindById(mascota.DueñoId);

        var historial = _context.HistorialesMedicos
            .FindOne(h => h.MascotaId == mascota.IdMascota);

        HistorialMedicoDto? historialDto = null;
        if (historial != null)
        {
            historialDto = ConstruirHistorialDto(historial);
        }

        return ConstruirMascotaDto(mascota, dueño, historialDto);
    }

    public IEnumerable<MascotaDto> ObtenerTodos()
    {
        var mascotas = _context.Mascotas.FindAll().ToList();

        var mascotasDtos = new List<MascotaDto>();
        foreach (var mascota in mascotas)
        {
            var dueño = _context.Dueños.FindById(mascota.DueñoId);
            var historial = _context.HistorialesMedicos
                .FindOne(h => h.MascotaId == mascota.IdMascota);

            HistorialMedicoDto? historialDto = historial != null ? ConstruirHistorialDto(historial) : null;

            mascotasDtos.Add(ConstruirMascotaDto(mascota, dueño, historialDto));
        }

        return mascotasDtos;
    }

    private HistorialMedicoDto ConstruirHistorialDto(HistorialMedico historial)
    {
        var serviciosMedicosDtos = new List<ServicioMedicoDto>();

        foreach (var servicioId in historial.ServiciosMedicosId)
        {
            var servicio = _context.ServiciosMedicos.FindById(servicioId);
            if (servicio == null) continue;

            var veterinario = _context.Veterinarios.FindById(servicio.VeterinariaId);
            var mascotaServicio = _context.Mascotas.FindById(servicio.MascotaId);

            var tratamientosDtos = new List<TratamientoDto>();
            foreach (var tId in servicio.TratamientosId)
            {
                var t = _context.Tratamientos.FindById(tId);
                if (t != null)
                {
                    // Si quieres, descomenta y agrega el DTO
                    /*
                    tratamientosDtos.Add(new TratamientoDto
                    {
                        IdTratamiento = t.IdTratamiento,
                        Nombre = t.Nombre,
                        Descripcion = t.Descripcion
                    });
                    */
                }
            }

            serviciosMedicosDtos.Add(new ServicioMedicoDto
            {
                ServicioMedicoId = servicio.ServicioMedicoId,
                Precio = servicio.Precio,
                Fecha = servicio.Fecha,
                Descripcion = servicio.Descripcion,
                VeterinarioDto = veterinario != null ? new VeterinarioDTO
                {
                    IdVeterinario = veterinario.veterinarioId,
                    Nombre = veterinario.Nombre,
                    Edad = veterinario.Edad,
                    Direccion = veterinario.Direccion,
                    Telefono = veterinario.Telefono
                } : null,
                MascotaDto = mascotaServicio != null ? new MascotaDto
                {
                    IdMascota = mascotaServicio.IdMascota,
                    Nombre = mascotaServicio.Nombre,
                    Edad = mascotaServicio.Edad,
                    Peso = mascotaServicio.Peso,
                    Especie = mascotaServicio.Especie,
                    Raza = mascotaServicio.Raza
                } : null,
                TratamientoDtos = tratamientosDtos
            });
        }

        return new HistorialMedicoDto
        {
            HistorialMedicoId = historial.HistorialMedicoId,
            Fecha = historial.FechaCreacion,
            Mascota = _context.Mascotas.FindById(historial.MascotaId) is var mascota && mascota != null ? new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza
                }
                : null,
            ServicioMedicoDtos = serviciosMedicosDtos
        };
    }

    private MascotaDto ConstruirMascotaDto(Mascota mascota, Dueño? dueño, HistorialMedicoDto? historialDto)
    {
        return new MascotaDto
        {
            IdMascota = mascota.IdMascota,
            Nombre = mascota.Nombre,
            Edad = mascota.Edad,
            Peso = mascota.Peso,
            Especie = mascota.Especie,
            Raza = mascota.Raza,
            Dueño = dueño != null ? new DueñoDto
            {
                IdPersona = dueño.IdPersona,
                Nombre = dueño.Nombre,
                Direccion = dueño.Direccion,
                Telefono = dueño.Telefono
            } : null,
            HistorialMedico = historialDto
        };
    }

    public bool Crear(CrearMascotaDto dto)
    {
        Console.WriteLine("llega aqui"+dto.DueñoId);
        var dueño = _context.Dueños.FindById(dto.DueñoId);
        Console.WriteLine("dososo"+dueño.Nombre);
        
        if (dueño == null) return false;
        Console.WriteLine(dueño.Nombre);

        var mascota = new Mascota
        {
            Nombre = dto.Nombre,
            Edad = dto.Edad,
            Peso = dto.Peso,
            Especie = dto.Especie,
            Raza = dto.Raza,
            DueñoId = dto.DueñoId
        };
        Console.WriteLine(mascota.Nombre);
        
        _context.Mascotas.Insert(mascota);
        
        var historial = new HistorialMedico
        {
            MascotaId = mascota.IdMascota,
            FechaCreacion = DateTime.Now,
            ServiciosMedicosId = new List<int>()
        };
        Console.WriteLine(historial.MascotaId+"Mascota");
        
        
        _context.HistorialesMedicos.Insert(historial);
        
        if (!dueño.MascotasId.Contains(mascota.IdMascota))
        {
            dueño.MascotasId.Add(mascota.IdMascota);
            _context.Dueños.Update(dueño);
        }
        
        return true;
    }

    public bool Actualizar(ActualizarMascotaDto dto)
    {
        var mascota = _context.Mascotas.FindById(dto.IdMascota);
        if (mascota == null) return false;

        var dueño = _context.Dueños.FindById(dto.DueñoId);
        if (dueño == null) return false;

        if (mascota.DueñoId != dto.DueñoId)
        {
            var dueñoAnterior = _context.Dueños.FindById(mascota.DueñoId);
            if (dueñoAnterior != null)
            {
                dueñoAnterior.MascotasId.Remove(mascota.IdMascota);
                _context.Dueños.Update(dueñoAnterior);
            }

            if (!dueño.MascotasId.Contains(mascota.IdMascota))
            {
                dueño.MascotasId.Add(mascota.IdMascota);
                _context.Dueños.Update(dueño);
            }
        }

        mascota.Nombre = dto.Nombre;
        mascota.Edad = dto.Edad;
        mascota.Peso = dto.Peso;
        mascota.Especie = dto.Especie;
        mascota.Raza = dto.Raza;
        mascota.DueñoId = dto.DueñoId;

        return _context.Mascotas.Update(mascota);
    }

    public bool Eliminar(int id)
    {
        var mascota = _context.Mascotas.FindById(id);
        if (mascota == null) return false;

        var dueño = _context.Dueños.FindById(mascota.DueñoId);
        if (dueño != null)
        {
            dueño.MascotasId.Remove(id);
            _context.Dueños.Update(dueño);
        }

        var historial = _context.HistorialesMedicos.FindOne(h => h.MascotaId == id);
        if (historial != null)
        {
            _context.HistorialesMedicos.Delete(historial.HistorialMedicoId);
        }

        return _context.Mascotas.Delete(id);
    }

    public bool AgregarMascota(int idDueño, int idMascota)
    {
        var dueño = _context.Dueños.FindById(idDueño);
        if (dueño == null) return false;

        var mascota = _context.Mascotas.FindById(idMascota);
        if (mascota == null) return false;

        if (mascota.DueñoId != 0)
        {
            var dueñoAnterior = _context.Dueños.FindById(mascota.DueñoId);
            if (dueñoAnterior != null)
            {
                dueñoAnterior.MascotasId.Remove(idMascota);
                _context.Dueños.Update(dueñoAnterior);
            }
        }

        if (!dueño.MascotasId.Contains(idMascota))
        {
            dueño.MascotasId.Add(idMascota);
            _context.Dueños.Update(dueño);
        }

        mascota.DueñoId = idDueño;
        return _context.Mascotas.Update(mascota);
    }
}