namespace GestionVeterinaria.Dtos.Veterinario;

public class VeterinarioDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public List<EspecialidadDto> especialidades { get; set; } = new List<EspecialidadDto>();
    //Se agrega una lista de los servicios medicos 
}