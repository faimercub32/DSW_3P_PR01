using DSW_3P_PR01.Persistence;
using DSW_3P_PR01.Repositories;
using DSW_3P_PR01.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "DSW_3P_PR01 | Gestor de eventos tecnológicos";
        document.Info.Description = "CRUD de eventos tecnológicos. Envía una fecha futura en formato ISO 8601.";
        return Task.CompletedTask;
    });
});
var conexion = builder.Configuration.GetConnectionString("Eventos")
    ?? throw new InvalidOperationException("Falta la cadena de conexión Eventos.");
builder.Services.AddDbContext<EventosDbContext>(options => options.UseSqlServer(conexion));
builder.Services.AddScoped<IEventoRepository, EventoSqlRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();

var app = builder.Build();
if (args is ["--importar-json", var ruta])
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<EventosDbContext>();
    var total = await ImportadorEventosJson.ImportarAsync(db, ruta);
    Console.WriteLine($"Eventos importados: {total}");
    return;
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "API de eventos v1"));
}

app.MapControllers();
app.Run();
