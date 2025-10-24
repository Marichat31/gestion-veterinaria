using GestionVeterinaria.Dtos.Vacuna;

namespace GestionVeterinaria.Services.Interfaces;

public interface IVacunaService
{
    VacunaDto? ObtenerPorId(int id);
    IEnumerable<VacunaDto> ObtenerTodos();
    bool Crear(CrearVacunaDto dto);
    bool Actualizar(ActualizarVacunaDto dto);
    bool Eliminar(int id);
    IEnumerable<VacunaDto> ObtenerVacunasDeMascota(int mascotaId);
}