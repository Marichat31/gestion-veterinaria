namespace GestionVeterinaria.Data.Models;

public class Veterinario : Persona
{
    public int EspecialidadId { get; set; }
    // 
    public List<int> ServiciosMedicosId { get; set; } = new();
}