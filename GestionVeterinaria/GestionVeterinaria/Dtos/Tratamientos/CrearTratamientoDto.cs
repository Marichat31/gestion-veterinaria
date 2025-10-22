namespace GestionVeterinaria.Dtos.Tratamientos;

public class CrearTratamientoDto
{
    public string NombreTratamiento { get; set; } = string.Empty;
    public string TipoTratamiento { get; set; } = string.Empty;
    public string DescripcionTratamiento { get; set; } = string.Empty;
    private List<string> Medicamento { get; set; } = new();
}