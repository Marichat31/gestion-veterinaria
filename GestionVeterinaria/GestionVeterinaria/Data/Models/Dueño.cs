namespace GestionVeterinaria.Data.Models;

public class Dueño : Persona
{
    public List<Mascota> Mascotas { get; set; }
    public Dueño(string nombre, int edad,string direccion,string telefono):base(nombre, edad,direccion,telefono)
    {
        this.Mascotas = null;
    }

    public Mascota registrarMascota(Mascota mascota)
    {
        Mascotas.Add(mascota);
        return mascota;
    }
}