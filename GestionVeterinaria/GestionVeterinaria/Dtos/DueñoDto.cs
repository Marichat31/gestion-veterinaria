using GestionVeterinaria.Data.Models;

namespace GestionVeterinaria.Dtos;

public class DueñoDto
{
    public int IdPersona { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public List<MascotaDto> Mascotas { get; set; } = new List<MascotaDto>();
}