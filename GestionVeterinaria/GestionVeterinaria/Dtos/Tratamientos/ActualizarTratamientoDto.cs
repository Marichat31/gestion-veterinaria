namespace GestionVeterinaria.Dtos.Tratamientos;

public class ActualizarTratamientoDto
{
    public int TratamientoId { get; set; }
    public string NombreTratamiento { get; set; } = string.Empty;
    public string TipoTratamiento { get; set; } = string.Empty;
    public string DescripcionTratamiento { get; set; } = string.Empty;
}