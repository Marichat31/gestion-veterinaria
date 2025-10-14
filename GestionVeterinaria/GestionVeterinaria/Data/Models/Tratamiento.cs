namespace GestionVeterinaria.Data.Models;

public class Tratamiento
{
    public int TratamientoId { get; set; }
    public string NombreTratamiento  { get; set; } = string.Empty;
    public  string TipoTratamiento { get; set; } = string.Empty;
    public string DescripcionTratamiento { get; set; } =  string.Empty;
    private List<string> Medicamentos { get; set; } = new();
}