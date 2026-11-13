using DSW_3P_PR01.Models;
using DSW_3P_PR01.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSW_3P_PR01.Repositories;

public class EventoSqlRepository(EventosDbContext db) : IEventoRepository
{
    public List<Evento> ObtenerTodos() =>
        db.Eventos.AsNoTracking().OrderBy(evento => evento.Id).ToList();

    public Evento? ObtenerPorId(int id) =>
        db.Eventos.AsNoTracking().SingleOrDefault(evento => evento.Id == id);

    public Evento Agregar(Evento nuevoEvento)
    {
        nuevoEvento.Id = 0;
        db.Eventos.Add(nuevoEvento);
        db.SaveChanges();
        return nuevoEvento;
    }

    public bool Actualizar(int id, Evento evento) =>
        db.Eventos.Where(existente => existente.Id == id)
            .ExecuteUpdate(columnas => columnas
                .SetProperty(existente => existente.Nombre, evento.Nombre)
                .SetProperty(existente => existente.Categoria, evento.Categoria)
                .SetProperty(existente => existente.Fecha, evento.Fecha)
                .SetProperty(existente => existente.Lugar, evento.Lugar)
                .SetProperty(existente => existente.CupoMaximo, evento.CupoMaximo)
                .SetProperty(existente => existente.Activo, evento.Activo)) == 1;

    public bool Eliminar(int id) =>
        db.Eventos.Where(evento => evento.Id == id).ExecuteDelete() == 1;
}
