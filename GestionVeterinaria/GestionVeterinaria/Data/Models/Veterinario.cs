namespace GestionVeterinaria.Data.Models;

public class Veterinario : Persona
{
    public int veterinarioId { get; set; }
    // 
    public List<int> ServiciosMedicosId { get; set; } = new();
    
}