using GestionVeterinaria.Dtos.HistorialMedico;
using GestionVeterinaria.Dtos.Vacuna;

namespace GestionVeterinaria.Dtos;

public class MascotaDto
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public double Peso { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    
    public DueñoDto? Dueño { get; set; }
    public HistorialMedicoDto HistorialMedico { get; set; }
    public List<VacunaDto>? Vacunas { get; set; }
}