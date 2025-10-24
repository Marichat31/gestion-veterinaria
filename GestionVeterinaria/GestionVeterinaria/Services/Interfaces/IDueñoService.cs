using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;

namespace GestionVeterinaria.Services.Interfaces;

public interface IDueñoService
{
    DueñoDto? ObtenerPorId(int id);
    IEnumerable<DueñoDto> ObtenerTodos();
    bool Crear(CrearDueñoDto dueño);
    bool Actualizar(ActualizarDueñoDto dueño);
    bool Eliminar(int id);
    IEnumerable<MascotaDto> ObtenerMascotasDeDueño(int dueñoId);
    IEnumerable<DueñoDto> DueñosConMasDeNMascotas(int numero);
}