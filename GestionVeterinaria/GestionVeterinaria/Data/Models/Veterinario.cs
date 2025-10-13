namespace GestionVeterinaria.Data.Models;

public class Veterinario : Persona
{

    private List<Especialidad> especialidades { get; set; }
    public Veterinario(string nombre, int edad,string direccion,string telefono):base(nombre, edad,direccion,telefono)
    {
        this.especialidades = null;
    }
    public void CrearReceta()
    {
        
    }
}