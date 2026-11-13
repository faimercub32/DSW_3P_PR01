using System.Data;
using System.Text.Json;
using DSW_3P_PR01.Models;
using Microsoft.EntityFrameworkCore;

namespace DSW_3P_PR01.Persistence;

internal static class ImportadorEventosJson
{
    public static async Task<int> ImportarAsync(EventosDbContext db, string ruta)
    {
        await using var archivo = File.OpenRead(ruta);
        var eventos = await JsonSerializer.DeserializeAsync<List<Evento>>(archivo)
            ?? throw new InvalidDataException("El archivo debe contener una lista de eventos.");

        if (eventos.Any(evento => evento is null || evento.Id <= 0 ||
            string.IsNullOrWhiteSpace(evento.Nombre) || evento.Fecha == default ||
            evento.CupoMaximo <= 0 || evento.Categoria is null || evento.Lugar is null) ||
            eventos.Select(evento => evento.Id).Distinct().Count() != eventos.Count)
        {
            throw new InvalidDataException("El archivo contiene eventos inválidos o IDs duplicados.");
        }

        if (eventos.Count == 0)
            return 0;

        await db.Database.OpenConnectionAsync();
        try
        {
            await using var transaccion = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            if (await db.Eventos.AnyAsync())
                throw new InvalidOperationException("La tabla Eventos debe estar vacía para importar el archivo.");

            await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Eventos ON");
            try
            {
                db.Eventos.AddRange(eventos);
                await db.SaveChangesAsync();
            }
            finally
            {
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Eventos OFF");
            }

            await transaccion.CommitAsync();
            return eventos.Count;
        }
        finally
        {
            await db.Database.CloseConnectionAsync();
        }
    }
}
