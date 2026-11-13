using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");
        if (evento.Categoria == null)
            throw new ArgumentException("La categoria no puede estar vacia");
        if (evento.Fecha <= DateTimeOffset.UtcNow)
            throw new ArgumentException("La fecha debe ser futura y válida.");
        if (evento.CupoMaximo <= 0)
            throw new ArgumentException("El cupo máximo debe ser mayor que cero.");
    }

    public Evento? ObtenerPorId(int id)
    {
        var Evento = _repository.ObtenerPorId(id);

        return Evento;
    }

    public List<Evento> ObtenerTodas()
    {
        return _repository.ObtenerTodos();
    }

    public Evento Agregar(Evento nuevaEvento)
    {
        Validar(nuevaEvento);

        return _repository.Agregar(nuevaEvento);
    }

    public bool Actualizar(int id, Evento Evento)
    {
        Validar(Evento);

        return _repository.Actualizar(id, Evento);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}