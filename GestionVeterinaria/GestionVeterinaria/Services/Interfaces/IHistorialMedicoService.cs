using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.ServicioMedico;

namespace GestionVeterinaria.Services.Interfaces;

public interface IHistorialMedicoService
{
    HistorialMedicoDto? ObtenerPorId(int id);
    IEnumerable<HistorialMedicoDto> ObtenerTodos();
    bool Crear(CrearHistorialMedicoDto dueño);
    bool Eliminar(int id);
    bool AddServicioMedico(int historialMedicoId, int  servicioMedicoId);
}