# Sistema de Gestión de Gastos

Sistema para control y gestión de gastos personales, implementado con arquitectura en N capas usando .NET.

## Arquitectura

El proyecto está organizado en 3 capas:

1. **SggApp.DAL**: Capa de acceso a datos que contiene:

   - Entidades: Usuarios, Categorías, Gastos, Presupuestos, Monedas
   - Repositorios: Implementación genérica y específica para cada entidad
   - Contexto de base de datos: ApplicationDbContext

2. **SggApp.BLL**: Capa de lógica de negocio que contiene:

   - Interfaces: Contratos para cada servicio
   - Servicios: Implementación de lógica de negocio para cada entidad

3. **SggApp.API**: Capa de presentación que contiene:
   - Controladores RESTful para cada entidad
   - Configuración de Swagger para documentación de la API

## Cómo ejecutar la aplicación

### Requisitos previos

- .NET 9.0 o superior
- SQL Server (local o remoto)

### Configuración

1. Clonar el repositorio
2. Configurar la cadena de conexión en `appsettings.json` en el proyecto SggApp.API:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=SggApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Ejecutar migraciones para crear la base de datos:

```
dotnet ef database update --project SggApp.DAL --startup-project SggApp.API
```

### Ejecución

1. Navegar a la carpeta del proyecto API
2. Ejecutar:

```
dotnet run
```

3. Acceder a Swagger para probar la API: `https://localhost:5001/swagger`

## Endpoints principales

### Usuarios

- `GET /api/usuarios` - Obtener todos los usuarios
- `GET /api/usuarios/{id}` - Obtener usuario por ID
- `GET /api/usuarios/porEmail?email={email}` - Obtener usuario por correo electrónico
- `POST /api/usuarios` - Crear nuevo usuario
- `PUT /api/usuarios/{id}` - Actualizar usuario existente
- `DELETE /api/usuarios/{id}` - Eliminar usuario

### Categorías

- `GET /api/categorias` - Obtener todas las categorías
- `GET /api/categorias/{id}` - Obtener categoría por ID
- `POST /api/categorias` - Crear nueva categoría
- `PUT /api/categorias/{id}` - Actualizar categoría existente
- `DELETE /api/categorias/{id}` - Eliminar categoría

### Gastos

- `GET /api/gastos` - Obtener todos los gastos
- `GET /api/gastos/{id}` - Obtener gasto por ID
- `GET /api/gastos/porUsuario/{usuarioId}` - Obtener gastos por usuario
- `GET /api/gastos/porCategoria/{categoriaId}` - Obtener gastos por categoría
- `GET /api/gastos/porPeriodo?fechaInicio={fecha}&fechaFin={fecha}` - Obtener gastos en un período
- `POST /api/gastos` - Crear nuevo gasto
- `PUT /api/gastos/{id}` - Actualizar gasto existente
- `DELETE /api/gastos/{id}` - Eliminar gasto

### Presupuestos

- `GET /api/presupuestos` - Obtener todos los presupuestos
- `GET /api/presupuestos/{id}` - Obtener presupuesto por ID
- `GET /api/presupuestos/porUsuario/{usuarioId}` - Obtener presupuestos por usuario
- `GET /api/presupuestos/porCategoria/{categoriaId}` - Obtener presupuestos por categoría
- `GET /api/presupuestos/vigentes?fecha={fecha}` - Obtener presupuestos vigentes
- `GET /api/presupuestos/{id}/verificarLimite` - Verificar si un presupuesto excede su límite
- `POST /api/presupuestos` - Crear nuevo presupuesto
- `PUT /api/presupuestos/{id}` - Actualizar presupuesto existente
- `DELETE /api/presupuestos/{id}` - Eliminar presupuesto

### Monedas

- `GET /api/monedas` - Obtener todas las monedas
- `GET /api/monedas/{id}` - Obtener moneda por ID
- `GET /api/monedas/porCodigo/{codigo}` - Obtener moneda por código ISO
- `POST /api/monedas` - Crear nueva moneda
- `PUT /api/monedas/{id}` - Actualizar moneda existente
- `DELETE /api/monedas/{id}` - Eliminar moneda
