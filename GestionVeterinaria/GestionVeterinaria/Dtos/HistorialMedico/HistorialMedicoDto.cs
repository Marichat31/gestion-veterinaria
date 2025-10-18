using GestionVeterinaria.Dtos.ServicioMedico;

namespace GestionVeterinaria.Dtos.HistorialMedico;

public class HistorialMedicoDto
{
    public int HistorialMedicoId { get; set; }
    public MascotaDto? Mascota { get; set; } = new MascotaDto();
    public List<ServicioMedicoDto> ServicioMedicoDtos { get; set; } = new List<ServicioMedicoDto>();
    public DateTime Fecha { get; set; }
}