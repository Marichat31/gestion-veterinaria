using LiteDB;

namespace GestionVeterinaria.Data.Models;

public abstract class Persona
{
    [BsonId] 
    public int IdPersona { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Direccion { get;set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}