namespace GestionVeterinaria.Dtos;

public class ActualizarEspecialidadDto
{
    public int Id { get; set; }
    public string NombreEspecialidad { get; set; } = string.Empty;
    public string DescripcionEspecialidad { get; set; } = string.Empty;

}