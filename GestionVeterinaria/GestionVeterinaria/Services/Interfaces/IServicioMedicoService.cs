using GestionVeterinaria.Dtos.ServicioMedico;

namespace GestionVeterinaria.Services.Interfaces;

public interface IServicioMedicoService
{
    ServicioMedicoDto? ObtenerPorId(int id);
    IEnumerable<ServicioMedicoDto> ObtenerTodos();
    bool Crear(CrearServicioMedicoDto dto);
    bool Actualizar(ActualizarServicioMedicoDto dto);
    bool Eliminar(int id);
}