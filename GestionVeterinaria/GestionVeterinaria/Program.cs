using GestionVeterinaria.Data;
using GestionVeterinaria.Services.Implementations;
using GestionVeterinaria.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LiteDbContext>();

// Registrar servicios
builder.Services.AddScoped<IDueñoService, DueñoService>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IHistorialMedicoService, HistorialMedicoService>();
builder.Services.AddScoped<IServicioMedicoService, ServicioMedicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();