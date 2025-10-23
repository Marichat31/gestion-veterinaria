namespace GestionVeterinaria.Data.Models;

public class Mascota
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public double Peso { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    
    public int DueñoId { get; set; }
    public int? HistorialMedicoId { get; set; }
    public List<int> VacunasId { get; set; } = new List<int>();
}