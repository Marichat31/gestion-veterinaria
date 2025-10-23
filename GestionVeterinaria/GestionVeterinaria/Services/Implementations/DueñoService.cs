using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Vacuna;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class DueñoService : IDueñoService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Dueño> _dueñoCrud;
    private readonly CrudGenerico<Mascota> _mascotaCrud;
    private readonly CrudGenerico<Vacuna> _vacunaCrud;

    public DueñoService(LiteDbContext context)
    {
        _context = context;
        _dueñoCrud = new CrudGenerico<Dueño>(context, context.Dueños);
        _mascotaCrud = new CrudGenerico<Mascota>(context, context.Mascotas);
        _vacunaCrud = new  CrudGenerico<Vacuna>(context, context.Vacunas);
    }

    public DueñoDto? ObtenerPorId(int id)
    {
        var dueño = _dueñoCrud.ObtenerPorId(id);
        if (dueño == null)
        {
            return null;
        }
        
        var mascotasDtos = new List<MascotaDto>();

        foreach (var mascotaId in dueño.MascotasId)
        {
            var mascota = _mascotaCrud.ObtenerPorId(mascotaId);
            if (mascota != null)
            {
                mascotasDtos.Add(DTOMapper.MapMascota(mascota));
            }
        }

        var dueñoDto = DTOMapper.MapDueño(dueño);
        dueñoDto.Mascotas = mascotasDtos;
        
        return dueñoDto;
    }

    public IEnumerable<DueñoDto> ObtenerTodos()
    {
        var dueños = _dueñoCrud.ObtenerTodos().ToList();
        var dueñosDtos = new List<DueñoDto>();
        
        foreach (var dueño in dueños)
        {
            var mascotasDtos = new List<MascotaDto>();

            foreach (var mascotaId in dueño.MascotasId)
            {
                var vacunas = _vacunaCrud.ObtenerTodos();
                var vacunasDtos = new List<VacunaDto>();

                foreach (var vacuna in vacunas)
                {
                    if (vacuna.MascotaId == mascotaId)
                    {
                        vacunasDtos.Add(DTOMapper.MapVacuna(vacuna));
                    }
                }
                
                
                var mascota = _mascotaCrud.ObtenerPorId(mascotaId);
                if (mascota != null)
                {
                    mascotasDtos.Add(DTOMapper.MapMascota(mascota, vacunasDtos));
                }
            }

            var dueñoDto = DTOMapper.MapDueño(dueño);
            dueñoDto.Mascotas = mascotasDtos;
            dueñosDtos.Add(dueñoDto);
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
        
        return _dueñoCrud.Crear(dueño);
    }

    public bool Actualizar(ActualizarDueñoDto dto)
    {
        var dueño = _dueñoCrud.ObtenerPorId(dto.Id);
        if (dueño == null)
        {
            return false;
        }

        dueño.Nombre = dto.Nombre;
        dueño.Edad = dto.Edad;
        dueño.Direccion = dto.Direccion;
        dueño.Telefono = dto.Telefono;
        
        return _dueñoCrud.Actualizar(dueño);
    }

    public bool Eliminar(int id)
    {
        return _dueñoCrud.Eliminar(id);
    }

    public IEnumerable<MascotaDto> ObtenerMascotasDeDueño(int dueñoId)
    {
        var dueño = _dueñoCrud.ObtenerPorId(dueñoId);
        if (dueño?.MascotasId == null)
        {
            return new List<MascotaDto>();
        }

        var mascotasDtos = new List<MascotaDto>();

        foreach (var mascotaId in dueño.MascotasId)
        {
            var mascota = _mascotaCrud.ObtenerPorId(mascotaId);
            if (mascota != null)
            {
                mascotasDtos.Add(DTOMapper.MapMascota(mascota));
            }
        }

        return mascotasDtos;
    }
}
