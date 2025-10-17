namespace GestionVeterinaria.Dtos.Veterinario;

public class VeterinarioDTO
{
    public int IdVeterinario { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public List<EspecialidadDto> especialidades { get; set; }
    //Se agrega una lista de los servicios medicos 
}