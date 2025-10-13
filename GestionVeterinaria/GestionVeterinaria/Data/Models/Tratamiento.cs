namespace GestionVeterinaria.Data.Models;

public class Tratamiento
{
    public string nombreTratamiento  { get; set; }
    public  string tipoTratamiento { get; set; }
    public string descripcionTratamiento { get; set; }
    private List<string> medicamentos { get; set; }
}