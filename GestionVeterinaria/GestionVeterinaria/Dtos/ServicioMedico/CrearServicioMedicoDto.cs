namespace GestionVeterinaria.Dtos.ServicioMedico;

public class CrearServicioMedicoDto
{
    public double Precio { get; set; }
    public DateTime Fecha { get; set; } 
    public string Descripcion { get; set; } = string.Empty;

    public int VeterinarioId { get; set; } 
    public int MascotaId { get; set; } 
}