using GestionVeterinaria.Data.Models;
using LiteDB;

namespace GestionVeterinaria.Data;

public class LiteDbContext 
{
    private readonly LiteDatabase _database;

    public LiteDbContext(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("LiteDb") ?? "Filename=Veterinaria.db;Connection=shared";
        var mapper = BsonMapper.Global;
        
        // Configurar con autoId para int
        mapper.Entity<Dueño>()
            .Id(x => x.IdPersona, autoId: true);
        
        mapper.Entity<Veterinario>()
            .Id(x => x.IdPersona, autoId: true);
        
        mapper.Entity<Mascota>()
            .Id(x => x.IdMascota, autoId: true);
        
        mapper.Entity<Tratamiento>()
            .Id(x => x.TratamientoId, autoId: true);
        
        mapper.Entity<ServicioMedico>()
            .Id(x => x.ServicioMedicoId, autoId: true);
        
        mapper.Entity<HistorialMedico>()
            .Id(x => x.HistorialMedicoId, autoId: true);
        
        mapper.Entity<Especialidad>()
            .Id(x => x.EspecialidadId, autoId: true);
        _database = new LiteDatabase(connectionString);
        
    }
    
    public ILiteCollection<Mascota> Mascotas => _database.GetCollection<Mascota>("Mascotas");
    public ILiteCollection<Dueño> Dueños => _database.GetCollection<Dueño>("Dueños");
    public ILiteCollection<Veterinario> Veterinarios => _database.GetCollection<Veterinario>("Veterinarios");
    public ILiteCollection<Especialidad> Especialidades => _database.GetCollection<Especialidad>("Especialidades");
    public ILiteCollection<ServicioMedico> ServiciosMedicos => _database.GetCollection<ServicioMedico>("ServiciosMedicos");
    public ILiteCollection<Tratamiento> Tratamientos => _database.GetCollection<Tratamiento>("Tratamientos");
    public ILiteCollection<HistorialMedico> HistorialesMedicos => _database.GetCollection<HistorialMedico>("HistorialesMedicos");
}