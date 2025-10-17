using GestionVeterinaria.Dtos.Veterinario;

namespace GestionVeterinaria.Services.Interfaces;

public interface IVeterinarioService
{
    VeterinarioDTO ObtenerPorId(int id);
    IEnumerable<VeterinarioDTO> ObtenerTodos();
    bool Crear(CrearVeterinarioDTO veterinario);
    bool Actualizar(ActualizarVeterinarioDTO veterinario);
    bool Eliminar(int id);
}