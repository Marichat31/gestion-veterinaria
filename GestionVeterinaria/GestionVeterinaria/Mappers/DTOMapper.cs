using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.Mascota;
using GestionVeterinaria.Dtos.ServicioMedico;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Vacuna;
using GestionVeterinaria.Dtos.Veterinario;

namespace GestionVeterinaria.Mappers;

public static class DTOMapper
{
    public static MascotaDto MapMascota(Mascota mascota, List<VacunaDto>? vacunas = null)
    {
        return new MascotaDto
        {
            IdMascota = mascota.IdMascota,
            Nombre = mascota.Nombre,
            Edad = mascota.Edad,
            Peso = mascota.Peso,
            Especie = mascota.Especie,
            Raza = mascota.Raza,
            Vacunas =  vacunas
        };
    }

    public static DueñoDto MapDueño(Dueño dueño)
    {
        return new DueñoDto
        {
            IdPersona = dueño.IdPersona,
            Nombre = dueño.Nombre,
            Edad = dueño.Edad,
            Direccion = dueño.Direccion,
            Telefono = dueño.Telefono
        };
    }

    public static TratamientoDto MapTratamiento(Tratamiento t)
    {
        return new TratamientoDto
        {
            TratamientoId = t.TratamientoId,
            NombreTratamiento = t.NombreTratamiento,
            TipoTratamiento = t.TipoTratamiento,
            DescripcionTratamiento = t.DescripcionTratamiento
        };
    }

    public static VeterinarioDTO MapVeterinario(Veterinario v)
    {
        return new VeterinarioDTO
        {
            Id = v.IdPersona,
            Nombre = v.Nombre,
            Edad = v.Edad,
            Direccion = v.Direccion,
            Telefono = v.Telefono
        };
    }

    public static ServicioMedicoDto MapServicioMedicoCompleto(ServicioMedico servicio, Veterinario? veterinario, Mascota? mascota, List<TratamientoDto> tratamientos)
    {
        return new ServicioMedicoDto
        {
            ServicioMedicoId = servicio.ServicioMedicoId,
            Precio = servicio.Precio,
            Fecha = servicio.Fecha,
            Descripcion = servicio.Descripcion,
            VeterinarioDto = veterinario != null ? MapVeterinario(veterinario) : null,
            MascotaDto = mascota != null ? MapMascota(mascota) : null,
            TratamientoDtos = tratamientos
        };
    }

    public static EspecialidadDto MapEspecialidad(Especialidad e)
    {
        return new EspecialidadDto
        {
            IdEspecialidad = e.EspecialidadId,
            NombreEspecialidad = e.Nombre,
            DescripcionEspecialidad = e.Descripcion
        };
    }

    public static HistorialMedicoDto MapHistorialMedico(HistorialMedico h)
    {
        return new HistorialMedicoDto
        {
            HistorialMedicoId = h.HistorialMedicoId,
            Fecha = h.FechaCreacion
        };
    }
    
    public static VacunaDto MapVacuna(Vacuna vacuna)
    {
        return new VacunaDto
        {
            VacunaId = vacuna.VacunaId,
            Nombre = vacuna.Nombre,
            Descripcion = vacuna.Descripcion,
            FechaAplicacion = vacuna.FechaAplicacion,
            MascotaId = vacuna.MascotaId
        };
    }
}
