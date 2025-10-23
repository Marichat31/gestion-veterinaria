namespace GestionVeterinaria.Dtos.Vacuna;

public class VacunaDto
{
    public int VacunaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; }
    public int MascotaId { get; set; }
    public MascotaDto? Mascota { get; set; }
}