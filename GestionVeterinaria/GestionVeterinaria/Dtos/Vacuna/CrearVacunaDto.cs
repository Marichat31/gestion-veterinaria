namespace GestionVeterinaria.Dtos.Vacuna;

public class CrearVacunaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; }
    public int MascotaId { get; set; }
}