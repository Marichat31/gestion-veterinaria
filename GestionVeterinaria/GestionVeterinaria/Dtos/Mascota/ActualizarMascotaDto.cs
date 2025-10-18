namespace GestionVeterinaria.Dtos.Mascota;

public class ActualizarMascotaDto
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public double Peso { get; set; }
    public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    
    public int DueñoId { get; set; }
}