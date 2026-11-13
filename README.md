# DSW_3P_PR01 · Gestor de eventos tecnológicos

API REST basada en la estructura del proyecto de referencia DSW_3P_PR02. La versión actual usa Entity Framework Core y SQL Server. Requiere el SDK de .NET 10 y una instancia de SQL Server.

## Ejecutar

Por defecto, la conexión apunta a `.\SQLEXPRESS`, usa autenticación de Windows y crea la base `DSW_3P_PR01`. Para otra instancia, ajusta `ConnectionStrings:Eventos` en `appsettings.json` o usa la variable de entorno `ConnectionStrings__Eventos`.

```powershell
dotnet tool restore
dotnet ef database update
dotnet run --launch-profile http
```

Abre [Swagger UI](http://localhost:5041/swagger). La interfaz Swagger se habilita en el entorno de desarrollo.

| Método | Ruta | Respuesta de éxito |
|---|---|---|
| GET | `/api/evento` | 200, lista de eventos |
| GET | `/api/evento/{id}` | 200, evento |
| POST | `/api/evento` | 201, evento y cabecera Location |
| PUT | `/api/evento/{id}` | 204 |
| DELETE | `/api/evento/{id}` | 204 |

POST y PUT requieren un nombre con texto, un cupo mayor que cero y una fecha futura en ISO 8601. Los datos inválidos devuelven 400. Una operación sobre un ID inexistente devuelve 404. El identificador lo asigna el servidor al crear y lo determina la ruta al actualizar.

```json
{
  "nombre": "Hackatón de software libre",
  "categoria": "Hackatón",
  "fecha": "2027-11-20T10:00:00-06:00",
  "lugar": "León",
  "cupoMaximo": 60,
  "activo": true
}
```

Para repetir el ejemplo en otra fecha, ajusta `fecha` a un valor futuro.

## Versiones e importación

El tag `v1-json` conserva la primera versión. Para probarla en otra carpeta:

```powershell
git worktree add --detach ../DSW_3P_PR01-json v1-json
cd ../DSW_3P_PR01-json
dotnet run --launch-profile http
```

La versión JSON crea `data/eventos.json` con los eventos del entorno local. Después de aplicar la migración a SQL Server, puedes importar ese archivo desde la versión actual:

```powershell
dotnet run -- --importar-json ../DSW_3P_PR01-json/data/eventos.json
```

La importación conserva los identificadores y requiere que la tabla `dbo.Eventos` esté vacía. Todos los eventos se guardan en una sola transacción.

La migración reemplaza `EventoJsonRepository` por `EventoSqlRepository`, incorpora `EventosDbContext` y agrega la configuración de SQL Server y la migración inicial. `EventoController`, `EventoService` y `IEventoRepository` son idénticos en las dos versiones.
