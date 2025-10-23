
# GestionVeterinaria .NET API

API RESTful desarrollada en .NET 9 con ASP.NET Core para gestionar información de una clínica veterinaria. Permite administrar dueños, mascotas, veterinarios, especialidades, vacunas, servicios médicos, tratamientos e historiales médicos mediante un sistema CRUD completo.

---

## 🗂️ Arquitectura del Proyecto

```
📦 GestionVeterinaria
├─ Controllers
├─ Data
│  ├─ LiteDbContext.cs
│  └─ Models
├─ Dtos
├─ Services
│  ├─ Implementations/
│  └─ Interfaces/
├─ Program.cs
```

---

## ⚙️ Configuración e Instalación

1. **Clona el repositorio**
   ```
   git clone https://github.com/Marichat31/gestion-veterinaria.git
   ```

2. **Abre el proyecto**

3. **Ejecuta la API**
   ```
   dotnet run
   ```

4. **Abre Swagger UI** para probar los endpoints:  
   [https://localhost:5016/swagger](https://localhost:5016/swagger)

---

## 🚀 Endpoints

### Dueños
- GET /api/duenos
- GET /api/duenos/{id}
- GET /api/duenos/{id}/mascotas
- POST /api/duenos
- PUT /api/duenos/{id}
- DELETE /api/duenos/{id}

### Especialidad
- GET /api/especialidad
- GET /api/especialidad/{id}
- POST /api/especialidad
- PUT /api/especialidad/{id}
- DELETE /api/especialidad/{id}

### Historiales Médicos
- GET /api/historialesmedicos
- GET /api/historialesmedicos/{id}
- POST /api/historialesmedicos
- DELETE /api/historialesmedicos/{id}
- POST /api/historialesmedicos/{historialMedicoId}/servicios/{servicioMedicoId}

### Mascotas
- GET /api/mascotas
- GET /api/mascotas/{id}
- POST /api/mascotas
- PUT /api/mascotas/{id}
- DELETE /api/mascotas/{id}
- POST /api/mascotas/dueño/{idDueño}/mascota/{idMascota}

### Servicios Médicos
- GET /api/serviciosmedicos
- GET /api/serviciosmedicos/{id}
- POST /api/serviciosmedicos
- PUT /api/serviciosmedicos/{id}
- DELETE /api/serviciosmedicos/{id}

### Tratamiento
- GET /api/tratamiento
- GET /api/tratamiento/{id}
- POST /api/tratamiento
- PUT /api/tratamiento/{id}
- DELETE /api/tratamiento/{id}

### Vacuna
- GET /api/vacuna
- GET /api/vacuna/{id}
- GET /api/vacuna/mascota/{mascotaId}
- POST /api/vacuna
- PUT /api/vacuna
- DELETE /api/vacuna/{id}

### Veterinario
- GET /api/veterinario
- GET /api/veterinario/{id}
- GET /api/veterinario/{id}/especialidades
- POST /api/veterinario
- PUT /api/veterinario/{id}
- DELETE /api/veterinario/{id}

---


## 👨‍💻 Autores

**Nombre:** Maria Juarez

**Nombre:** Omar Garcia

**Materia:** Bases de datos Avanzadas

**Profesor:** Neira Sanchez Rojas

**Institución:** Novauniversitas   

