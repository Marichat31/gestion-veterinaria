using GestionVeterinaria.Data.Models;
using LiteDB;

namespace GestionVeterinaria.Data;

public class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("LiteDb") ?? "Filename=Veterinaria.db;Connection=shared";
        _database = new LiteDatabase(connectionString);
        
    }
    
    public ILiteCollection<Mascota> Mascotas => _database.GetCollection<Mascota>("Mascotas");
    public ILiteCollection<Dueño> Dueños => _database.GetCollection<Dueño>("Dueños");
    public ILiteCollection<Veterinario> Veterinarios => _database.GetCollection<Veterinario>("Veterinarios");
    public ILiteCollection<Especialidad> Especialidades => _database.GetCollection<Especialidad>("Especialidades");
    public ILiteCollection<ServicioMedico> ServiciosMedicos => _database.GetCollection<ServicioMedico>("ServiciosMedicos");
    public ILiteCollection<Tratamiento> Tratamientos => _database.GetCollection<Tratamiento>("Tratamientos");
    public ILiteCollection<HistorialMedico> HistorialesMedicos => _database.GetCollection<HistorialMedico>("HistorialesMedicos");

    public void Dispose()
    {
        _database?.Dispose();
    }
    
}