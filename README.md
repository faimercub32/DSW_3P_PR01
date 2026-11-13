# DSW_3P_PR01 · Gestor de eventos tecnológicos

API REST basada en la estructura del proyecto de referencia DSW_3P_PR02. Requiere el SDK de .NET 10.

## Ejecutar

```powershell
dotnet run --launch-profile http
```

Abre [Swagger UI](http://localhost:5041/swagger). El repositorio crea `data/eventos.json` en el primer acceso a la API.

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
