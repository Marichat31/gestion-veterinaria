namespace GestionVeterinaria.Dtos.Veterinario;

public class ActualizarVeterinarioDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    
    public int IdEspecialidad { get; set; }
    
}