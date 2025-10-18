namespace GestionVeterinaria.Data.Models;

public class Especialidad
{
    public int EspecialidadId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    
    public int VeterinarioId { get; set; }
    public Veterinario veterinario { get; set; } =  new();
}