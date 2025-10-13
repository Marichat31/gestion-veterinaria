namespace GestionVeterinaria.Data.Models;

public abstract class Persona
{
    public string nombre { get; set; }
    public int edad { get; set; }
    public string direccion { get;set; }
    public string telefono { get; set; }
    public Persona(string nombre,int edad,string direccion,string telefono)
    {
        this.nombre = nombre;
        this.edad = edad;
        this.direccion = direccion;
        this.telefono = telefono;
    }
}