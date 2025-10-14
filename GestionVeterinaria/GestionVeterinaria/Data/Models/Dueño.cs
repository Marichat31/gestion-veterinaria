namespace GestionVeterinaria.Data.Models;

public class Dueño : Persona
{
    public List<int> MascotasId { get; set; } = new();
}