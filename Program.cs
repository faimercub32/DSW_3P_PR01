using DSW_3P_PR01.Repositories;
using DSW_3P_PR01.Services;

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
builder.Services.AddSingleton<IEventoRepository, EventoJsonRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "API de eventos v1"));
}

app.MapControllers();
app.Run();
