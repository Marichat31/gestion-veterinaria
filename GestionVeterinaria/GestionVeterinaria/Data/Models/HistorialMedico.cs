namespace GestionVeterinaria.Data.Models;

public class HistorialMedico
{
    public int HistorialMedicoId { get; set; }
    public int MascotaId { get; set; }
    public List<int> ServiciosMedicosId { get; set; } = new();
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}