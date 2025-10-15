using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos;

namespace GestionVeterinaria.Services.Interfaces;

public interface IEspecialidadService
{
    EspecialidadDto ObtenerPorId(int id);
    IEnumerable<EspecialidadDto> ObtenerTodos();
    bool Crear(CrearEspecialidadDto especialidad);
    bool Actualizar(ActualizarEspecialidadDto especialidad);
    bool Eliminar(int id);
}