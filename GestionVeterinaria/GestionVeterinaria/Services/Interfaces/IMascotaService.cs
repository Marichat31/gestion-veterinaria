using GestionVeterinaria.Dtos;
using GestionVeterinaria.Dtos.Mascota;

namespace GestionVeterinaria.Services.Interfaces;

public interface IMascotaService
{
    MascotaDto? ObtenerPorId(int id);
    IEnumerable<MascotaDto> ObtenerTodos();
    bool Crear(CrearMascotaDto dto);
    bool Actualizar(ActualizarMascotaDto dto);
    bool Eliminar(int id);
    bool AgregarMascota(int idDueño, int idMascota);
    public IEnumerable<MascotaDto> MascotasConVacunaVencida();
    IEnumerable<MascotaDto> FiltroPorEspecieyRango(string especie, int edadMin, int edadMax);
}