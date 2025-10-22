using GestionVeterinaria.Dtos.Tratamientos;

namespace GestionVeterinaria.Services.Interfaces;

public interface ITratamientoService
{
    TratamientoDto? ObtenerPorId(int id);
    IEnumerable<TratamientoDto> ObtenerTodos();
    bool Crear(CrearTratamientoDto dto);
    bool Actualizar(ActualizarTratamientoDto dto);
    bool Eliminar(int id);
}