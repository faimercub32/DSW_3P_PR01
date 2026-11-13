using System.Text.Json;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public class EventoJsonRepository : IEventoRepository
{
    private readonly string _ruta;
    private readonly object _candado = new();
    private static readonly JsonSerializerOptions Opciones = new() { WriteIndented = true };

    public EventoJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "data", "eventos.json");
        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);
        if (!File.Exists(_ruta))
            Guardar([]);
    }

    public List<Evento> ObtenerTodos()
    {
        lock (_candado)
            return Leer();
    }

    public Evento? ObtenerPorId(int id)
    {
        lock (_candado)
            return Leer().Find(evento => evento.Id == id);
    }

    public Evento Agregar(Evento nuevoEvento)
    {
        lock (_candado)
        {
            var eventos = Leer();
            nuevoEvento.Id = eventos.Count == 0 ? 1 : eventos.Max(evento => evento.Id) + 1;
            eventos.Add(nuevoEvento);
            Guardar(eventos);
            return nuevoEvento;
        }
    }

    public bool Actualizar(int id, Evento evento)
    {
        lock (_candado)
        {
            var eventos = Leer();
            var indice = eventos.FindIndex(existente => existente.Id == id);
            if (indice < 0)
                return false;

            evento.Id = id;
            eventos[indice] = evento;
            Guardar(eventos);
            return true;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado)
        {
            var eventos = Leer();
            if (eventos.RemoveAll(evento => evento.Id == id) == 0)
                return false;

            Guardar(eventos);
            return true;
        }
    }

    private List<Evento> Leer() =>
        JsonSerializer.Deserialize<List<Evento>>(File.ReadAllText(_ruta)) ?? [];

    private void Guardar(List<Evento> eventos)
    {
        var temporal = _ruta + ".tmp";
        File.WriteAllText(temporal, JsonSerializer.Serialize(eventos, Opciones));
        File.Move(temporal, _ruta, overwrite: true);
    }
}
