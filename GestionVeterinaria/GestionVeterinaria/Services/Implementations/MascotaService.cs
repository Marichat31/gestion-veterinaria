using GestionVeterinaria.Data;
using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.Mascota;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Vacuna;
using GestionVeterinaria.Dtos.Veterinario;
using GestionVeterinaria.Services.Interfaces;
using GestionVeterinaria.Mappers;

namespace GestionVeterinaria.Services.Implementations;

public class MascotaService : IMascotaService
{
    private readonly LiteDbContext _context;
    private readonly CrudGenerico<Mascota> _mascotaCrud;
    private readonly CrudGenerico<Dueño> _dueñoCrud;
    private readonly CrudGenerico<HistorialMedico> _historialCrud;
    private readonly CrudGenerico<Vacuna> _vacunaCrud;

    public MascotaService(LiteDbContext context)
    {
        _context = context;
        _mascotaCrud = new CrudGenerico<Mascota>(context, context.Mascotas);
        _dueñoCrud = new CrudGenerico<Dueño>(context, context.Dueños);
        _historialCrud = new CrudGenerico<HistorialMedico>(context, context.HistorialesMedicos);
        _vacunaCrud = new CrudGenerico<Vacuna>(context, context.Vacunas);
    }

    public MascotaDto? ObtenerPorId(int id)
    {
        var mascota = _mascotaCrud.ObtenerPorId(id);
        if (mascota == null) return null;

        var dueño = _dueñoCrud.ObtenerPorId(mascota.DueñoId);

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
        var mascotas = _mascotaCrud.ObtenerTodos().ToList();

        var mascotasDtos = new List<MascotaDto>();
        foreach (var mascota in mascotas)
        {
            var dueño = _dueñoCrud.ObtenerPorId(mascota.DueñoId);
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
            var mascotaServicio = _mascotaCrud.ObtenerPorId(servicio.MascotaId);

            var tratamientosDtos = new List<TratamientoDto>();
            foreach (var tId in servicio.TratamientosId)
            {
                var t = _context.Tratamientos.FindById(tId);
                if (t != null)
                {
                    tratamientosDtos.Add(DTOMapper.MapTratamiento(t));
                }
            }

            serviciosMedicosDtos.Add(DTOMapper.MapServicioMedicoCompleto(
                servicio, 
                veterinario, 
                mascotaServicio, 
                tratamientosDtos
            ));
        }

        var historialDto = DTOMapper.MapHistorialMedico(historial);
        historialDto.Mascota = _mascotaCrud.ObtenerPorId(historial.MascotaId) is var mascota && mascota != null 
            ? DTOMapper.MapMascota(mascota) 
            : null;
        historialDto.ServicioMedicoDtos = serviciosMedicosDtos;

        return historialDto;
    }

    private MascotaDto ConstruirMascotaDto(Mascota mascota, Dueño? dueño, HistorialMedicoDto? historialDto)
    {
        var vacunasDtos = new List<VacunaDto>();
    
        if (mascota.VacunasId != null && mascota.VacunasId.Count > 0)
        {
            foreach (var vacunaId in mascota.VacunasId)
            {
                var vacuna = _vacunaCrud.ObtenerPorId(vacunaId);
                if (vacuna != null)
                {
                    vacunasDtos.Add(DTOMapper.MapVacuna(vacuna));
                }
            }
        }

        var mascotaDto = DTOMapper.MapMascota(mascota, vacunasDtos.Count > 0 ? vacunasDtos : null);
        mascotaDto.Dueño = dueño != null ? DTOMapper.MapDueño(dueño) : null;
        mascotaDto.HistorialMedico = historialDto;

        return mascotaDto;
    }


    public bool Crear(CrearMascotaDto dto)
    {
        Console.WriteLine("llega aqui" + dto.DueñoId);
        var dueño = _dueñoCrud.ObtenerPorId(dto.DueñoId);
        Console.WriteLine("dososo" + dueño?.Nombre);

        if (dueño == null) return false;
        Console.WriteLine(dueño.Nombre);

        var mascota = new Mascota
        {
            Nombre = dto.Nombre,
            Edad = dto.Edad,
            Peso = dto.Peso,
            Especie = dto.Especie,
            Raza = dto.Raza,
            DueñoId = dto.DueñoId,
            VacunasId = new List<int>()
        };
        Console.WriteLine(mascota.Nombre);

        _mascotaCrud.Crear(mascota);

        var historial = new HistorialMedico
        {
            MascotaId = mascota.IdMascota,
            FechaCreacion = DateTime.Now,
            ServiciosMedicosId = new List<int>()
        };
        Console.WriteLine(historial.MascotaId + "Mascota");

        _historialCrud.Crear(historial);

        if (!dueño.MascotasId.Contains(mascota.IdMascota))
        {
            dueño.MascotasId.Add(mascota.IdMascota);
            _dueñoCrud.Actualizar(dueño);
        }

        return true;
    }

    public bool Actualizar(ActualizarMascotaDto dto)
    {
        var mascota = _mascotaCrud.ObtenerPorId(dto.IdMascota);
        if (mascota == null) return false;

        var dueño = _dueñoCrud.ObtenerPorId(dto.DueñoId);
        if (dueño == null) return false;

        if (mascota.DueñoId != dto.DueñoId)
        {
            var dueñoAnterior = _dueñoCrud.ObtenerPorId(mascota.DueñoId);
            if (dueñoAnterior != null)
            {
                dueñoAnterior.MascotasId.Remove(mascota.IdMascota);
                _dueñoCrud.Actualizar(dueñoAnterior);
            }

            if (!dueño.MascotasId.Contains(mascota.IdMascota))
            {
                dueño.MascotasId.Add(mascota.IdMascota);
                _dueñoCrud.Actualizar(dueño);
            }
        }

        mascota.Nombre = dto.Nombre;
        mascota.Edad = dto.Edad;
        mascota.Peso = dto.Peso;
        mascota.Especie = dto.Especie;
        mascota.Raza = dto.Raza;
        mascota.DueñoId = dto.DueñoId;

        return _mascotaCrud.Actualizar(mascota);
    }

    public bool Eliminar(int id)
    {
        var mascota = _mascotaCrud.ObtenerPorId(id);
        if (mascota == null) return false;

        var dueño = _dueñoCrud.ObtenerPorId(mascota.DueñoId);
        if (dueño != null)
        {
            dueño.MascotasId.Remove(mascota.IdMascota);
            _dueñoCrud.Actualizar(dueño);
        }

        var historial = _context.HistorialesMedicos.FindOne(h => h.MascotaId == mascota.IdMascota);
        if (historial != null)
        {
            _context.HistorialesMedicos.Delete(historial.HistorialMedicoId);
        }

        return _mascotaCrud.Eliminar(id);
    }

    public bool AgregarMascota(int idDueño, int idMascota)
    {
        throw new NotImplementedException();
    }
    
    
    public IEnumerable<MascotaDto> MascotasConVacunaVencida()
    {
        var resultado = new List<MascotaDto>();
        var mascotas = _context.Mascotas.FindAll().ToList();
        var vacunas = _context.Vacunas.FindAll().ToList();
        foreach (var mascota in mascotas)
        {
            bool tieneVencida = false;
            foreach (var vacuna in vacunas)
            {
                if (vacuna.MascotaId == mascota.IdMascota)
                {
                    if (vacuna.FechaAplicacion < DateTime.Now.AddYears(-1))
                    {
                        tieneVencida = true;
                        break;
                    }
                }
            }

            if (tieneVencida)
            {
                resultado.Add(new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza,
                });
            }

            
        }
        return resultado;
    }

    public IEnumerable<MascotaDto> FiltroPorEspecieyRango(int idMascota)
    {
        throw new NotImplementedException();
    }


    public IEnumerable<MascotaDto> FiltroPorEspecieyRango(string especie, int edadMin, int edadMax)
    {
        var todasMascotas = _context.Mascotas.FindAll().ToList();
        var resultado = new List<MascotaDto>();
        foreach (var mascota in todasMascotas)
        {
            if (mascota.Especie.Equals(especie, StringComparison.OrdinalIgnoreCase) && mascota.Edad >= edadMin &&
                mascota.Edad <= edadMax)
            {
                resultado.Add(new MascotaDto
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    Edad = mascota.Edad,
                    Peso = mascota.Peso,
                    Especie = mascota.Especie,
                    Raza = mascota.Raza,
                });
            }
        }
        return resultado;
    } 
}
