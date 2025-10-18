using GestionVeterinaria.Data.Models;
using GestionVeterinaria.Dtos.Tratamientos;
using GestionVeterinaria.Dtos.Veterinario;

namespace GestionVeterinaria.Dtos.ServicioMedico;

public class ServicioMedicoDto
{
    public int ServicioMedicoId { get; set; }
    public double Precio { get; set; }
    public DateTime Fecha { get; set; } 
    public string Descripcion { get; set; } = string.Empty;

    public VeterinarioDTO? VeterinarioDto { get; set; } = null!;

    public MascotaDto? MascotaDto { get; set; } = null!;

    public List<TratamientoDto> TratamientoDtos { get; set; } = new List<TratamientoDto>();
}