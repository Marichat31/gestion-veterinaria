namespace GestionVeterinaria.Data.Models;

public class ServicioMedico
{
    public DateTime fecha { get; set; }
    public Veterinario veterinario { get; set; }
    public String descripcion { get; set; } 
    public Mascota mascota { get; set; }
    public List<Tratamiento> tratamientos { get; set; }
    

}