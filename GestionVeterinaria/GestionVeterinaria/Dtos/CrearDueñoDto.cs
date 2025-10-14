namespace GestionVeterinaria.Dtos;

public class CrearDueñoDto
{
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
}