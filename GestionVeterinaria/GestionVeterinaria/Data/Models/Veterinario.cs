namespace GestionVeterinaria.Data.Models;

public class Veterinario : Persona
{
    // 
    public List<int> ServiciosMedicosId { get; set; } = new();
    public List<Especialidad> ListaEspecialidades { get; set; } = new();
    public List<int> EspecialidadesId { get; set; } = new();
}