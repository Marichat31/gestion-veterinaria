namespace GestionVeterinaria.Dtos.ServicioMedico;

public class ActualizarServicioMedicoDto
{
    public int ServicioMedicoId { get; set; }
    public double Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int VeterinarioId { get; set; } 
}