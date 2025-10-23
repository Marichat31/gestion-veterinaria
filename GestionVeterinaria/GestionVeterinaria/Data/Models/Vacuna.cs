namespace GestionVeterinaria.Data.Models;

public class Vacuna
{
    public int VacunaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; } = DateTime.Now;
    public int MascotaId { get; set; }
}