namespace GestionVeterinaria.Data.Models;

public class ServicioMedico
{
    public int ServicioMedicoId { get; set; }
    public double Precio { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string Descripcion { get; set; } = string.Empty;
    
    public int VeterinariaId { get; set; }
    public int MascotaId { get; set; }
    public List<int> TratamientosId { get; set; } = new();
    
}