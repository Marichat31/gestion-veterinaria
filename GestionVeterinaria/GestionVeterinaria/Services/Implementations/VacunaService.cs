using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Vacuna;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class VacunaService : IVacunaService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Vacuna> _vacunaCrud;
    private readonly CrudGenerico<Mascota> _mascotaCrud;

    public VacunaService(LiteDbContext context)
    {
        _context = context;
        _vacunaCrud = new CrudGenerico<Vacuna>(context, context.Vacunas);
        _mascotaCrud = new CrudGenerico<Mascota>(context, context.Mascotas);
    }

    public VacunaDto? ObtenerPorId(int id)
    {
        var vacuna = _vacunaCrud.ObtenerPorId(id);
        if (vacuna == null) return null;

        var mascota = _mascotaCrud.ObtenerPorId(vacuna.MascotaId);
        
        var vacunaDto = DTOMapper.MapVacuna(vacuna);
        vacunaDto.Mascota = mascota != null ? DTOMapper.MapMascota(mascota) : null;
        
        return vacunaDto;
    }

    public IEnumerable<VacunaDto> ObtenerTodos()
    {
        var vacunas = _vacunaCrud.ObtenerTodos().ToList();
        var vacunasDtos = new List<VacunaDto>();

        foreach (var vacuna in vacunas)
        {
            var mascota = _mascotaCrud.ObtenerPorId(vacuna.MascotaId);
            
            var vacunaDto = DTOMapper.MapVacuna(vacuna);
            vacunaDto.Mascota = mascota != null ? DTOMapper.MapMascota(mascota) : null;
            
            vacunasDtos.Add(vacunaDto);
        }

        return vacunasDtos;
    }

    public bool Crear(CrearVacunaDto dto)
    {
        var mascota = _mascotaCrud.ObtenerPorId(dto.MascotaId);
        if (mascota == null) return false;

        var vacuna = new Vacuna
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            FechaAplicacion = dto.FechaAplicacion,
            MascotaId = dto.MascotaId
        };

        _vacunaCrud.Crear(vacuna);
        
        if (!mascota.VacunasId.Contains(vacuna.VacunaId))
        {
            mascota.VacunasId.Add(vacuna.VacunaId);
            _mascotaCrud.Actualizar(mascota);
        }

        return true;
    }

    public bool Actualizar(ActualizarVacunaDto dto)
    {
        var vacuna = _vacunaCrud.ObtenerPorId(dto.VacunaId);
        if (vacuna == null) return false;

        var mascotaNueva = _mascotaCrud.ObtenerPorId(dto.MascotaId);
        if (mascotaNueva == null) return false;

        // Si cambió la mascota, actualizar las listas
        if (vacuna.MascotaId != dto.MascotaId)
        {
            var mascotaAnterior = _mascotaCrud.ObtenerPorId(vacuna.MascotaId);
            if (mascotaAnterior != null)
            {
                mascotaAnterior.VacunasId.Remove(vacuna.VacunaId);
                _mascotaCrud.Actualizar(mascotaAnterior);
            }

            if (!mascotaNueva.VacunasId.Contains(vacuna.VacunaId))
            {
                mascotaNueva.VacunasId.Add(vacuna.VacunaId);
                _mascotaCrud.Actualizar(mascotaNueva);
            }
        }

        vacuna.Nombre = dto.Nombre;
        vacuna.Descripcion = dto.Descripcion;
        vacuna.FechaAplicacion = dto.FechaAplicacion;
        vacuna.MascotaId = dto.MascotaId;

        return _vacunaCrud.Actualizar(vacuna);
    }

    public bool Eliminar(int id)
    {
        var vacuna = _vacunaCrud.ObtenerPorId(id);
        if (vacuna == null) return false;

        var mascota = _mascotaCrud.ObtenerPorId(vacuna.MascotaId);
        if (mascota != null)
        {
            mascota.VacunasId.Remove(vacuna.VacunaId);
            _mascotaCrud.Actualizar(mascota);
        }

        return _vacunaCrud.Eliminar(id);
    }

    public IEnumerable<VacunaDto> ObtenerVacunasDeMascota(int mascotaId)
    {
        var mascota = _mascotaCrud.ObtenerPorId(mascotaId);
        if (mascota?.VacunasId == null)
        {
            return new List<VacunaDto>();
        }

        var vacunasDtos = new List<VacunaDto>();
        foreach (var vacunaId in mascota.VacunasId)
        {
            var vacuna = _vacunaCrud.ObtenerPorId(vacunaId);
            if (vacuna != null)
            {
                vacunasDtos.Add(DTOMapper.MapVacuna(vacuna));
            }
        }

        return vacunasDtos;
    }
}
